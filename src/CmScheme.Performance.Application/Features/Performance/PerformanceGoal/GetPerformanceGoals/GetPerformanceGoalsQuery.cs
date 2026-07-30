using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.PerformanceGoal.GetPerformanceGoals;

public sealed record GetPerformanceGoalsQuery : IQuery<Result<IReadOnlyList<Core.Dtos.PerformanceGoalDto>>>
{
    public int UserAccountId { get; init; }
}
