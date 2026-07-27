using Ardalis.Result;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Certificate.Application.Features.Certificate.ListCertificates;

public sealed class ListCertificatesQueryHandler(ICertificateQueryDbContext dbContext)
    : IQueryHandler<ListCertificatesQuery, Result<List<CertificateApplicationDto>>>
{
    public async ValueTask<Result<List<CertificateApplicationDto>>> Handle(ListCertificatesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.CertificateApplication> query = dbContext.CertificateApplications;

        if (request.ApplicantId.HasValue)
        {
            query = query.Where(c => c.ApplicantId == request.ApplicantId.Value);
        }

        List<CertificateApplicationDto> items = await query
            .Select(c => new CertificateApplicationDto
            {
                CertificateId = c.CertificateId,
                ApplicantId = c.ApplicantId,
                ApplicantName = c.ApplicantName,
                ProgramName = c.ProgramName,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                DurationDays = c.DurationDays,
                Status = c.Status,
                CreatedOn = c.CreatedOn
            })
            .ToListAsync(cancellationToken);

        return Result<List<CertificateApplicationDto>>.Success(items);
    }
}
