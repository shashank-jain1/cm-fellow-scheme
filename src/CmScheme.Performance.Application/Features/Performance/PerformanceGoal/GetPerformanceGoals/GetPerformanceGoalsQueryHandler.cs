using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.PerformanceGoal.GetPerformanceGoals;

public sealed class GetPerformanceGoalsQueryHandler(
    IPerformanceQueryDbContext dbContext)
    : IQueryHandler<GetPerformanceGoalsQuery, Result<IReadOnlyList<PerformanceGoalDto>>>
{
    public async ValueTask<Result<IReadOnlyList<PerformanceGoalDto>>> Handle(
        GetPerformanceGoalsQuery query,
        CancellationToken cancellationToken)
    {
        List<PerformanceGoalDto> goals = await dbContext.PerformanceGoals
            .AsNoTracking()
            .Where(g => g.UserAccountId == query.UserAccountId)
            .Select(g => new PerformanceGoalDto(
                g.PerformanceGoalId,
                g.UserAccountId,
                g.GoalTitle,
                g.Description,
                g.TargetDate,
                g.Status,
                g.ReviewCycleId,
                g.CreatedOn))
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<PerformanceGoalDto>>.Success(goals);
    }
}
