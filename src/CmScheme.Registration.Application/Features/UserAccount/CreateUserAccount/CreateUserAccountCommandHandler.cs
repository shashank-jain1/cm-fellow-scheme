using System.Security.Cryptography;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.UserAccount.CreateUserAccount;

public sealed class CreateUserAccountCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<CreateUserAccountCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateUserAccountCommand request,
        CancellationToken cancellationToken)
    {
        Applicant? applicant = await dbContext.Applicants
            .FirstOrDefaultAsync(a => a.ApplicantId == request.ApplicantId, cancellationToken);

        if (applicant is null)
        {
            return Result<int>.NotFound("Applicant not found.");
        }

        bool usernameExists = await dbContext.UserAccounts
            .AnyAsync(ua => ua.Username == request.Username, cancellationToken);

        if (usernameExists)
        {
            return Result<int>.Conflict("Username already exists.");
        }

        string passwordHash = HashPassword(request.Password);

        Core.Entities.UserAccount userAccount = new Core.Entities.UserAccount
        {
            ApplicantId = request.ApplicantId,
            Username = request.Username,
            PasswordHash = passwordHash,
            Role = request.Role,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = request.CreatedBy
        };

        dbContext.UserAccounts.Add(userAccount);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(userAccount.UserAccountId);
    }

    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        using Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);
        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);
        return Convert.ToBase64String(hashBytes);
    }
}
