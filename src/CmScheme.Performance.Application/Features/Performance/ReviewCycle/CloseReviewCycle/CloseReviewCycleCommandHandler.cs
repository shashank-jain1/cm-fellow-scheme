using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.ReviewCycle.CloseReviewCycle;

public sealed class CloseReviewCycleCommandHandler(
    IPerformanceCommandDbContext dbContext)
    : ICommandHandler<CloseReviewCycleCommand, Result>
{
    public async ValueTask<Result> Handle(
        CloseReviewCycleCommand request,
        CancellationToken cancellationToken)
    {
        PerformanceReviewCycle? cycle = await dbContext.PerformanceReviewCycles
            .FirstOrDefaultAsync(c => c.ReviewCycleId == request.ReviewCycleId, cancellationToken);

        if (cycle is null)
        {
            return Result.NotFound("Review cycle not found.");
        }

        cycle.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
