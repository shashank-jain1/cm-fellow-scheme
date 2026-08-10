using System.Security.Cryptography;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using UserAccountEntity = CmScheme.Registration.Core.Entities.UserAccount;

namespace CmScheme.Registration.Application.Features.UserAccount.AdminResetPassword;

public sealed class AdminResetPasswordCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<AdminResetPasswordCommand, Result>
{
    public async ValueTask<Result> Handle(
        AdminResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        UserAccountEntity? userAccount = await dbContext.UserAccounts
            .FirstOrDefaultAsync(ua => ua.UserAccountId == request.UserAccountId, cancellationToken);

        if (userAccount is null)
        {
            return Result.NotFound("User account not found.");
        }

        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            request.NewPassword, salt, 100_000, HashAlgorithmName.SHA256, 32);
        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);

        userAccount.PasswordHash = Convert.ToBase64String(hashBytes);

        // Any outstanding self-service reset link is void once an admin sets the password.
        userAccount.PasswordResetToken = null;
        userAccount.PasswordResetTokenExpiry = null;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
