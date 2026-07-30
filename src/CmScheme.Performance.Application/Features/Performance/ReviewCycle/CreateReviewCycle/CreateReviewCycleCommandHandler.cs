using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Entities;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.ReviewCycle.CreateReviewCycle;

public sealed class CreateReviewCycleCommandHandler(
    IPerformanceCommandDbContext dbContext)
    : ICommandHandler<CreateReviewCycleCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateReviewCycleCommand request,
        CancellationToken cancellationToken)
    {
        PerformanceReviewCycle cycle = new()
        {
            CycleName = request.CycleName,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsActive = true,
            CreatedOn = DateTime.UtcNow
        };

        dbContext.PerformanceReviewCycles.Add(cycle);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(cycle.ReviewCycleId);
    }
}
