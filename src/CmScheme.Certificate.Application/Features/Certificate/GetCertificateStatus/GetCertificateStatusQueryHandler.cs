using Ardalis.Result;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Certificate.Application.Features.Certificate.GetCertificateStatus;

public sealed class GetCertificateStatusQueryHandler(ICertificateQueryDbContext dbContext)
    : IQueryHandler<GetCertificateStatusQuery, Result<CertificateApplicationDto?>>
{
    public async ValueTask<Result<CertificateApplicationDto?>> Handle(GetCertificateStatusQuery request, CancellationToken cancellationToken)
    {
        CertificateApplicationDto? certificate = await dbContext.CertificateApplications
            .Where(c => c.CertificateId == request.CertificateId)
            .Select(c => new CertificateApplicationDto
            {
                CertificateId = c.CertificateId,
                ApplicantId = c.ApplicantId,
                ApplicantName = c.ApplicantName,
                ProgramName = c.ProgramName,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                DurationDays = c.DurationDays,
                VerifiedBy = c.VerifiedBy,
                CertificateIssueDate = c.CertificateIssueDate,
                Status = c.Status,
                CreatedOn = c.CreatedOn
            })
            .FirstOrDefaultAsync(cancellationToken);

        return Result<CertificateApplicationDto?>.Success(certificate);
    }
}
