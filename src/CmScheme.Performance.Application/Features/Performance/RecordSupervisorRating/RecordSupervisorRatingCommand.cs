using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.RecordSupervisorRating;

public sealed record RecordSupervisorRatingCommand : ICommand<Result>
{
    public int PerformanceEvaluationId { get; init; }
    public decimal SupervisorRating { get; init; }
    public string EvaluatedBy { get; init; } = null!;
}
