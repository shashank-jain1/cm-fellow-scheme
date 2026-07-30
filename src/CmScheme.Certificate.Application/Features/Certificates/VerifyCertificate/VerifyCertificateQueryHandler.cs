using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Entities;
using CmScheme.Common.Core.Services;

namespace CmScheme.Certificate.Application.Features.Certificates.VerifyCertificate;

public sealed class VerifyCertificateQueryHandler(
    ICertificateQueryDbContext queryDbContext,
    ICertificateTemplateService certificateTemplateService)
    : IQueryHandler<VerifyCertificateQuery, Result<CertificateVerificationResult>>
{
    public async ValueTask<Result<CertificateVerificationResult>> Handle(
        VerifyCertificateQuery request,
        CancellationToken cancellationToken)
    {
        CertificateVerification? verification = await queryDbContext.CertificateVerifications
            .FirstOrDefaultAsync(v => v.CertificateNumber == request.CertificateNumber, cancellationToken);

        if (verification is not null)
        {
            return Result.Success(new CertificateVerificationResult
            {
                IsValid = true,
                CertificateNumber = verification.CertificateNumber,
                FellowName = verification.FellowName,
                ProgramName = verification.ProgramName,
                IssueDate = verification.IssueDate,
                QrCodeUrl = verification.VerificationUrl
            });
        }

        if (int.TryParse(request.CertificateNumber.Replace("CERT-", ""), out int certificateId))
        {
            CertificateApplication? certificate = await queryDbContext.CertificateApplications
                .FirstOrDefaultAsync(c => c.CertificateId == certificateId, cancellationToken);

            if (certificate is not null)
            {
                return Result.Success(new CertificateVerificationResult
                {
                    IsValid = false,
                    CertificateNumber = request.CertificateNumber,
                    FellowName = certificate.ApplicantName,
                    ProgramName = certificate.ProgramName,
                    IssueDate = certificate.CertificateIssueDate ?? certificate.CreatedOn,
                    QrCodeUrl = null
                });
            }
        }

        return Result.NotFound("Certificate not found.");
    }
}
