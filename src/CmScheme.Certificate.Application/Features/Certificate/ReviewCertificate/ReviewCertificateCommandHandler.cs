using Ardalis.Result;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Entities;
using CmScheme.Common.Core;
using Mediator;
using Microsoft.EntityFrameworkCore;

using CmScheme.Common.Core.Services;

namespace CmScheme.Certificate.Application.Features.Certificate.ReviewCertificate;

public sealed class ReviewCertificateCommandHandler(
    ICertificateCommandDbContext dbContext,
    INotificationService notificationService)
    : ICommandHandler<ReviewCertificateCommand, Result>
{
    public async ValueTask<Result> Handle(ReviewCertificateCommand request, CancellationToken cancellationToken)
    {
        CertificateApplication? certificate = await dbContext.CertificateApplications
            .FirstOrDefaultAsync(c => c.CertificateId == request.CertificateId, cancellationToken);

        if (certificate is null)
        {
            return Result.NotFound("Certificate application not found.");
        }

        certificate.Status = request.Status;
        certificate.VerifiedBy = request.VerifiedBy;

        if (string.Equals(request.Status, Statuses.Certificate.Approved, StringComparison.OrdinalIgnoreCase))
        {
            certificate.CertificateIssueDate = DateTime.UtcNow;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        if (string.Equals(request.Status, Statuses.Certificate.Approved, StringComparison.OrdinalIgnoreCase))
        {
            var (subject, body, sms) = NotificationTemplates.CertificateApproved(
                certificate.ApplicantName ?? $"Applicant #{certificate.ApplicantId}",
                certificate.ProgramName);

            await notificationService.SendEmailAsync(
                $"applicant{certificate.ApplicantId}@program.gov.in",
                subject,
                body,
                cancellationToken);
        }

        return Result.Success();
    }
}
