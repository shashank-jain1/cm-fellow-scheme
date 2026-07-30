using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.ReviewCycle.GetActiveReviewCycle;

public sealed class GetActiveReviewCycleQueryHandler(
    IPerformanceQueryDbContext dbContext)
    : IQueryHandler<GetActiveReviewCycleQuery, Result<ReviewCycleDto>>
{
    public async ValueTask<Result<ReviewCycleDto>> Handle(
        GetActiveReviewCycleQuery request,
        CancellationToken cancellationToken)
    {
        ReviewCycleDto? cycle = await dbContext.PerformanceReviewCycles
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderByDescending(c => c.CreatedOn)
            .Select(c => new ReviewCycleDto
            {
                ReviewCycleId = c.ReviewCycleId,
                CycleName = c.CycleName,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                IsActive = c.IsActive,
                CreatedOn = c.CreatedOn
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (cycle is null)
        {
            return Result.NotFound("No active review cycle found.");
        }

        return Result<ReviewCycleDto>.Success(cycle);
    }
}
