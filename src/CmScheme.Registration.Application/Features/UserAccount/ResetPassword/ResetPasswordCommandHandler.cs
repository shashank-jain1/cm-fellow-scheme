using System.Security.Cryptography;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using UserAccountEntity = CmScheme.Registration.Core.Entities.UserAccount;

namespace CmScheme.Registration.Application.Features.UserAccount.ResetPassword;

public sealed class ResetPasswordCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<ResetPasswordCommand, Result>
{
    public async ValueTask<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        UserAccountEntity? userAccount = await dbContext.UserAccounts
            .FirstOrDefaultAsync(ua => ua.PasswordResetToken == request.Token, cancellationToken);

        // Do not distinguish "unknown token" from "expired token" — either way the caller
        // has not proven ownership, and a narrower message would leak which tokens exist.
        if (userAccount is null
            || userAccount.PasswordResetTokenExpiry is null
            || userAccount.PasswordResetTokenExpiry < DateTime.UtcNow)
        {
            return Result.Invalid(new ValidationError(
                "This password reset link is invalid or has expired. Please request a new one."));
        }

        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            request.NewPassword, salt, 100_000, HashAlgorithmName.SHA256, 32);
        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);

        userAccount.PasswordHash = Convert.ToBase64String(hashBytes);

        // Single use: burn the token so the link cannot be replayed.
        userAccount.PasswordResetToken = null;
        userAccount.PasswordResetTokenExpiry = null;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
