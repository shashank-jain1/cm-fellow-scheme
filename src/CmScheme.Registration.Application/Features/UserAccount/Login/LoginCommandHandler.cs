using System.Security.Cryptography;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.UserAccount.Login;

public sealed class LoginCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<LoginCommand, Result<LoginResult>>
{
    public async ValueTask<Result<LoginResult>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        Core.Entities.UserAccount? userAccount = await dbContext.UserAccounts
            .FirstOrDefaultAsync(
                ua => ua.Username == request.Username && ua.IsActive,
                cancellationToken);

        if (userAccount is null)
        {
            return Result<LoginResult>.NotFound("Invalid username or password.");
        }

        bool passwordValid = VerifyPassword(request.Password, userAccount.PasswordHash);

        if (!passwordValid)
        {
            return Result<LoginResult>.NotFound("Invalid username or password.");
        }

        return Result<LoginResult>.Success(new LoginResult
        {
            UserAccountId = userAccount.UserAccountId,
            Username = userAccount.Username,
            Role = userAccount.Role,
        });
    }

    private static bool VerifyPassword(string password, string storedHash)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        using Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        for (int i = 0; i < 32; i++)
        {
            if (hashBytes[i + 16] != hash[i])
            {
                return false;
            }
        }

        return true;
    }
}
