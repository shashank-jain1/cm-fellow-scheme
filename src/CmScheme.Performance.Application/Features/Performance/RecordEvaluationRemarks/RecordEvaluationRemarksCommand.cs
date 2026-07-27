using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.RecordEvaluationRemarks;

public sealed record RecordEvaluationRemarksCommand : ICommand<Result>
{
    public int PerformanceEvaluationId { get; init; }
    public string EvaluationRemarks { get; init; } = null!;
    public string EvaluatedBy { get; init; } = null!;
}
