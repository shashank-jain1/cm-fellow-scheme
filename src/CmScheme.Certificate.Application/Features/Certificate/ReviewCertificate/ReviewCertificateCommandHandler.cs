using Ardalis.Result;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Entities;
using CmScheme.Common.Core;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Certificate.Application.Features.Certificate.ReviewCertificate;

public sealed class ReviewCertificateCommandHandler(ICertificateCommandDbContext dbContext)
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

        return Result.Success();
    }
}
