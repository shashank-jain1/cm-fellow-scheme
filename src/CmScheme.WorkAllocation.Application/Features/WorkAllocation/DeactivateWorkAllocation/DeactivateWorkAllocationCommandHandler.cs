using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.WorkAllocation.Core.Data;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.DeactivateWorkAllocation;

public sealed class DeactivateWorkAllocationCommandHandler(IWorkAllocationCommandDbContext dbContext)
    : ICommandHandler<DeactivateWorkAllocationCommand, Result>
{
    public async ValueTask<Result> Handle(DeactivateWorkAllocationCommand request, CancellationToken cancellationToken)
    {
        Core.Entities.WorkAllocation? workAllocation = await dbContext.WorkAllocations
            .FirstOrDefaultAsync(w => w.WorkAllocationId == request.WorkAllocationId, cancellationToken);

        if (workAllocation is null)
        {
            return Result.NotFound("Work allocation not found.");
        }

        if (!workAllocation.ActiveStatus)
        {
            return Result.Invalid(new ValidationError("Work allocation is already inactive."));
        }

        if (workAllocation.Status != "completed")
        {
            return Result.Invalid(new ValidationError("Cannot deactivate a work allocation that is not completed."));
        }

        workAllocation.ActiveStatus = false;
        workAllocation.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
