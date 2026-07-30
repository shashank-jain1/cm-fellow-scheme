using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.PerformanceGoal.CreatePerformanceGoal;

public sealed record CreatePerformanceGoalCommand : ICommand<Result<int>>
{
    public int UserAccountId { get; init; }
    public string GoalTitle { get; init; } = null!;
    public string? Description { get; init; }
    public DateTime TargetDate { get; init; }
    public int? ReviewCycleId { get; init; }
}
