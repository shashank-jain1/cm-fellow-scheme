using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CmScheme.Registration.Application.Features.UserAccount.Login;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.Auth;

public sealed class LoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public sealed class Login(IConfiguration configuration)
{
    public static async Task<IResult> Handle(
        LoginRequest request,
        ISender sender,
        IConfiguration config,
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
        string token = GenerateJwtToken(user, config);

        return Results.Ok(new
        {
            token,
            userAccountId = user.UserAccountId,
            username = user.Username,
            role = user.Role,
        });
    }

    private static string GenerateJwtToken(LoginResult user, IConfiguration config)
    {
        string jwtKey = config["Jwt:Key"]
            ?? "CmScheme@2025!SecretKey#ForJwtTokenGeneration$VeryLong32Chars+";
        string? jwtIssuer = config["Jwt:Issuer"];
        string? jwtAudience = config["Jwt:Audience"];
        string? jwtExpiryMinutes = config["Jwt:ExpiryMinutes"];

        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.UserAccountId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ];

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
