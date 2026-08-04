using System.Security.Cryptography;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Registration.ApproveRegistration;

public sealed class ApproveRegistrationCommandHandler(
    IRegistrationCommandDbContext dbContext,
    INotificationService notificationService)
    : ICommandHandler<ApproveRegistrationCommand, Result>
{
    private const string DefaultPassword = "Fellow@123";

    public async ValueTask<Result> Handle(
        ApproveRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        Applicant? applicant = await dbContext.Applicants
            .FirstOrDefaultAsync(a => a.ApplicantId == request.ApplicantId, cancellationToken);

        if (applicant is null)
        {
            return Result.NotFound("Applicant not found.");
        }

        if (applicant.Status == Statuses.Registration.Approved)
        {
            return Result.Conflict("Registration is already approved.");
        }

        applicant.Status = Statuses.Registration.Approved;
        applicant.ModifiedOn = DateTime.UtcNow;
        applicant.ModifiedBy = request.ApprovedBy;

        string role = GetRoleFromTraining(applicant.AppliedForTraining);

        Core.Entities.UserAccount userAccount = new Core.Entities.UserAccount
        {
            ApplicantId = applicant.ApplicantId,
            Username = applicant.MobileNumber,
            PasswordHash = HashPassword(DefaultPassword),
            Role = role,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = request.ApprovedBy
        };

        dbContext.UserAccounts.Add(userAccount);
        await dbContext.SaveChangesAsync(cancellationToken);

        var (subject, body, sms) = NotificationTemplates.RegistrationApproved($"{applicant.FirstName} {applicant.LastName}");
        await notificationService.SendEmailAsync(
            applicant.EmailId,
            subject,
            body + $"\n\nUsername: {applicant.MobileNumber}\nDefault Password: {DefaultPassword}\nPlease log in and change your password.",
            cancellationToken);

        await notificationService.SendSmsAsync(
            applicant.MobileNumber,
            sms,
            cancellationToken);

        return Result.NoContent();
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
