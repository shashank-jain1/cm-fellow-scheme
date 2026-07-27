using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Registration.ApproveRegistration;

public sealed class ApproveRegistrationCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<ApproveRegistrationCommand, Result>
{
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

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
