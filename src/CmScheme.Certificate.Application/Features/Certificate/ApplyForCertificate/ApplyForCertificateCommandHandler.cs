using Ardalis.Result;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Entities;
using CmScheme.Common.Core;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Certificate.ApplyForCertificate;

public sealed class ApplyForCertificateCommandHandler(ICertificateCommandDbContext dbContext)
    : ICommandHandler<ApplyForCertificateCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(ApplyForCertificateCommand request, CancellationToken cancellationToken)
    {
        CertificateApplication certificate = new CertificateApplication
        {
            ApplicantId = request.ApplicantId,
            ApplicantName = request.ApplicantName,
            ProgramName = request.ProgramName,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            DurationDays = request.DurationDays,
            Status = Statuses.Certificate.Applied,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = request.CreatedBy
        };

        dbContext.CertificateApplications.Add(certificate);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(certificate.CertificateId);
    }
}
