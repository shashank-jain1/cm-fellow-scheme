using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Entities;
using CmScheme.Common.Core.Services;

namespace CmScheme.Certificate.Application.Features.Certificates.GenerateExperienceLetter;

public sealed class GenerateExperienceLetterCommandHandler(
    ICertificateCommandDbContext dbContext,
    ICertificateTemplateService certificateTemplateService,
    IFileUploadService fileUploadService)
    : ICommandHandler<GenerateExperienceLetterCommand, Result<string>>
{
    public async ValueTask<Result<string>> Handle(
        GenerateExperienceLetterCommand request,
        CancellationToken cancellationToken)
    {
        CertificateApplication? certificate = await dbContext.CertificateApplications
            .FirstOrDefaultAsync(c => c.ApplicantId == request.ApplicantId, cancellationToken);

        if (certificate is null)
        {
            return Result.NotFound("Certificate application not found for the specified applicant.");
        }

        string designation = "Fellow";

        byte[] pdfBytes = await certificateTemplateService.GenerateExperienceLetterAsync(
            certificate.ApplicantName,
            designation,
            request.StartDate,
            request.EndDate,
            request.SupervisorName,
            cancellationToken);

        string fileName = $"EXP-{certificate.CertificateId:D4}_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf";
        using MemoryStream stream = new(pdfBytes);
        string filePath = await fileUploadService.UploadAsync(
            stream,
            fileName,
            "application/pdf",
            "experience-letters",
            cancellationToken);

        return Result.Success(fileUploadService.GetFileUrl(filePath));
    }
}
