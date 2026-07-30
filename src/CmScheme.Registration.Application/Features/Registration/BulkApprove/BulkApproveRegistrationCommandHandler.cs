using System.Security.Cryptography;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Registration.BulkApprove;

public sealed class BulkApproveRegistrationCommandHandler(
    IRegistrationCommandDbContext dbContext,
    INotificationService notificationService)
    : ICommandHandler<BulkApproveRegistrationCommand, Result<int>>
{
    private const string DefaultPassword = "Fellow@123";

    public async ValueTask<Result<int>> Handle(
        BulkApproveRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        if (request.ApplicantIds is null || request.ApplicantIds.Count == 0)
        {
            return Result.Invalid(new ValidationError("At least one Applicant ID is required."));
        }

        bool isApprove = string.Equals(request.Action, "Approve", StringComparison.OrdinalIgnoreCase);
        bool isReject = string.Equals(request.Action, "Reject", StringComparison.OrdinalIgnoreCase);

        if (!isApprove && !isReject)
        {
            return Result.Invalid(new ValidationError("Action must be 'Approve' or 'Reject'."));
        }

        List<Applicant> applicants = await dbContext.Applicants
            .Where(a => request.ApplicantIds.Contains(a.ApplicantId))
            .ToListAsync(cancellationToken);

        int processedCount = 0;

        foreach (Applicant applicant in applicants)
        {
            if (isApprove && applicant.Status != Statuses.Registration.Approved)
            {
                applicant.Status = Statuses.Registration.Approved;
                applicant.ModifiedOn = DateTime.UtcNow;
                applicant.ModifiedBy = request.PerformedBy;

                string role = GetRoleFromTraining(applicant.AppliedForTraining);

                Core.Entities.UserAccount userAccount = new Core.Entities.UserAccount
                {
                    ApplicantId = applicant.ApplicantId,
                    Username = applicant.MobileNumber,
                    PasswordHash = HashPassword(DefaultPassword),
                    Role = role,
                    IsActive = true,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = request.PerformedBy
                };

                dbContext.UserAccounts.Add(userAccount);

                await notificationService.SendEmailAsync(
                    applicant.EmailId,
                    "Welcome to CM Fellow Program",
                    $"Dear {applicant.FirstName} {applicant.LastName},\n\n" +
                    $"Your registration has been approved.\n\n" +
                    $"Username: {applicant.MobileNumber}\n" +
                    $"Password: {DefaultPassword}\n\n" +
                    $"Please log in and change your password.\n\n" +
                    $"Best regards,\nCM Fellow Program Team",
                    cancellationToken);

                processedCount++;
            }
            else if (isReject && applicant.Status != Statuses.Registration.Rejected)
            {
                applicant.Status = Statuses.Registration.Rejected;
                applicant.ModifiedOn = DateTime.UtcNow;
                applicant.ModifiedBy = request.PerformedBy;

                processedCount++;
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(processedCount);
    }

    private static string GetRoleFromTraining(int appliedForTraining)
    {
        return appliedForTraining switch
        {
            1 => "Fellow",
            2 => "Supervisor",
            3 => "Administrator",
            _ => "Fellow"
        };
    }

    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        using Rfc2898DeriveBytes pbkdf2 = new(password, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);
        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);
        return Convert.ToBase64String(hashBytes);
    }
}
