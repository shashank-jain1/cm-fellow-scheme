using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Entities;
using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;

namespace CmScheme.Certificate.Application.Features.Certificates.GenerateCompletionCertificate;

public sealed class GenerateCompletionCertificateCommandHandler(
    ICertificateCommandDbContext dbContext,
    ICertificateTemplateService certificateTemplateService,
    IFileUploadService fileUploadService)
    : ICommandHandler<GenerateCompletionCertificateCommand, Result<string>>
{
    public async ValueTask<Result<string>> Handle(
        GenerateCompletionCertificateCommand request,
        CancellationToken cancellationToken)
    {
        CertificateApplication? certificate = await dbContext.CertificateApplications
            .FirstOrDefaultAsync(c => c.ApplicantId == request.ApplicantId, cancellationToken);

        if (certificate is null)
        {
            return Result.NotFound("Certificate application not found for the specified applicant.");
        }

        if (!string.Equals(certificate.Status, Statuses.Certificate.Approved, StringComparison.OrdinalIgnoreCase))
        {
            return Result.Invalid(new ValidationError("Certificate must be approved before generation."));
        }

        string certificateNumber = $"CERT-{certificate.CertificateId:D4}";
        DateTime completionDate = certificate.EndDate;

        byte[] pdfBytes = await certificateTemplateService.GenerateCompletionCertificateAsync(
            certificate.ApplicantName,
            certificate.ProgramName,
            completionDate,
            certificateNumber,
            cancellationToken);

        string fileName = $"{certificateNumber}_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf";
        using MemoryStream stream = new(pdfBytes);
        string filePath = await fileUploadService.UploadAsync(
            stream,
            fileName,
            "application/pdf",
            "certificates",
            cancellationToken);

        certificate.CertificatePdfPath = filePath;
        certificate.CertificateIssueDate = DateTime.UtcNow;
        certificate.Status = Statuses.Certificate.Issued;
        certificate.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(fileUploadService.GetFileUrl(filePath));
    }
}
