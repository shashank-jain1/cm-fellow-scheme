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
            .FirstOrDefaultAsync(ua => ua.Username == request.Email || ua.Applicant!.EmailId == request.Email, cancellationToken);

        if (userAccount is null)
        {
            return Result.NotFound("No account found with this email.");
        }

        byte[] salt = RandomNumberGenerator.GetBytes(16);
        using Rfc2898DeriveBytes pbkdf2 = new(request.NewPassword, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);
        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);

        userAccount.PasswordHash = Convert.ToBase64String(hashBytes);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
