using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.PerformanceGoal.UpdatePerformanceGoal;

public sealed record UpdatePerformanceGoalCommand : ICommand<Result>
{
    public int PerformanceGoalId { get; init; }
    public string Status { get; init; } = null!;
}
