using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.WorkAllocation.Core.Data;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.AssignWorkAllocation;

public sealed class AssignWorkAllocationCommandHandler(IWorkAllocationCommandDbContext dbContext)
    : ICommandHandler<AssignWorkAllocationCommand, Result>
{
    public async ValueTask<Result> Handle(AssignWorkAllocationCommand request, CancellationToken cancellationToken)
    {
        Core.Entities.WorkAllocation? workAllocation = await dbContext.WorkAllocations
            .FirstOrDefaultAsync(w => w.WorkAllocationId == request.WorkAllocationId, cancellationToken);

        if (workAllocation is null)
        {
            return Result.NotFound("Work allocation not found.");
        }

        workAllocation.AssignedToUserId = request.AssignedToUserId;
        workAllocation.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
