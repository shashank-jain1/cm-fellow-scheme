using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.GetReviewHistory;

public sealed record GetReviewHistoryQuery : IQuery<Result<List<ReviewHistoryDto>>>
{
    public int PerformanceEvaluationId { get; init; }
}

public sealed record ReviewHistoryDto
{
    public int PerformanceReviewHistoryId { get; init; }
    public int PerformanceEvaluationId { get; init; }
    public string Action { get; init; } = null!;
    public string PreviousLevel { get; init; } = null!;
    public string NewLevel { get; init; } = null!;
    public string PreviousStatus { get; init; } = null!;
    public string NewStatus { get; init; } = null!;
    public string? PerformedBy { get; init; }
    public string? Remarks { get; init; }
    public DateTime PerformedOn { get; init; }
}
