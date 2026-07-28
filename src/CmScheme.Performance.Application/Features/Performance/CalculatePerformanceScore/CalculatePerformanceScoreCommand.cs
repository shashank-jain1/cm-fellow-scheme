using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.CalculatePerformanceScore;

public sealed record CalculatePerformanceScoreCommand : ICommand<Result>
{
    public int PerformanceEvaluationId { get; init; }
}
