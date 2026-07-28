using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.SubmitPerformanceReview;

public sealed record SubmitPerformanceReviewCommand : ICommand<Result>
{
    public int PerformanceEvaluationId { get; init; }
    public string Action { get; init; } = null!;
    public string PerformedBy { get; init; } = null!;
    public string? Remarks { get; init; }
}
