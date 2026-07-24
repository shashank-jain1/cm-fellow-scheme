using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Registration.RejectRegistration;

public sealed class RejectRegistrationCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<RejectRegistrationCommand, Result>
{
    public async ValueTask<Result> Handle(
        RejectRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        Applicant? applicant = await dbContext.Applicants
            .FirstOrDefaultAsync(a => a.ApplicantId == request.ApplicantId, cancellationToken);

        if (applicant is null)
        {
            return Result.NotFound("Applicant not found.");
        }

        if (applicant.Status == "Rejected")
        {
            return Result.Conflict("Registration is already rejected.");
        }

        applicant.Status = "Rejected";
        applicant.ModifiedOn = DateTime.UtcNow;
        applicant.ModifiedBy = request.RejectedBy;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
