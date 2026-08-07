using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CmScheme.Registration.Application.Features.UserAccount.Login;
using CmScheme.Registration.Core.Data;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.Auth;

public sealed class LoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public sealed class Login
{
    public static async Task<IResult> Handle(
        LoginRequest request,
        ISender sender,
        IConfiguration config,
        IRegistrationCommandDbContext dbContext,
        CancellationToken cancellationToken)
    {
        ValueTask<Result<LoginResult>> result = sender.Send(
            new LoginCommand
            {
                Username = request.Username,
                Password = request.Password,
            },
            cancellationToken);

        Result<LoginResult> loginResult = await result;

        if (!loginResult.IsSuccess)
        {
            return loginResult.ToApiResult();
        }

        LoginResult user = loginResult.Value;

        var moduleAccess = await dbContext.UserModuleAccesses
            .Where(uma => uma.UserAccountId == user.UserAccountId && uma.IsActive)
            .Join(dbContext.ModuleMasters,
                uma => uma.ModuleMasterId,
                mm => mm.ModuleMasterId,
                (uma, mm) => new ModuleAccessEntry
                {
                    ModuleCode = mm.ModuleCode,
                    CanRead = uma.CanRead,
                    CanWrite = uma.CanWrite,
                    CanApprove = uma.CanApprove,
                    CanExport = uma.CanExport,
                })
            .ToListAsync(cancellationToken);

        string token = GenerateJwtToken(user, config, moduleAccess);

        var modules = moduleAccess.ToDictionary(
            x => x.ModuleCode,
            x => new
            {
                canRead = x.CanRead,
                canWrite = x.CanWrite,
                canApprove = x.CanApprove,
                canExport = x.CanExport,
            });

        return Results.Ok(new
        {
            token,
            userAccountId = user.UserAccountId,
            username = user.Username,
            role = user.Role,
            modules,
        });
    }

    private static string GenerateJwtToken(LoginResult user, IConfiguration config,
        List<ModuleAccessEntry> moduleAccess)
    {
        string? jwtKey = config["Jwt:Key"];
        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException("Jwt:Key is not configured. Set it in appsettings.json or User Secrets.");
        }
        string? jwtIssuer = config["Jwt:Issuer"];
        string? jwtAudience = config["Jwt:Audience"];
        string? jwtExpiryMinutes = config["Jwt:ExpiryMinutes"];

        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.UserAccountId.ToString()),
            new Claim("UserAccountId", user.UserAccountId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ];

        foreach (var access in moduleAccess)
        {
            string permissions = string.Join(",",
                new[] { access.CanRead, access.CanWrite, access.CanApprove, access.CanExport }
                    .Select((v, i) => v ? new[] { "R", "W", "A", "E" }[i] : "")
                    .Where(s => !string.IsNullOrEmpty(s)));

            if (!string.IsNullOrEmpty(permissions))
            {
                claims.Add(new Claim($"module:{access.ModuleCode}", permissions));
            }
        }

        int expiryMinutes = int.TryParse(jwtExpiryMinutes, out int parsed) ? parsed : 480;

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public sealed class ModuleAccessEntry
{
    public string ModuleCode { get; set; } = null!;
    public bool CanRead { get; set; }
    public bool CanWrite { get; set; }
    public bool CanApprove { get; set; }
    public bool CanExport { get; set; }
}
