using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.ReviewCycle.CloseReviewCycle;

public sealed record CloseReviewCycleCommand : ICommand<Result>
{
    public int ReviewCycleId { get; init; }
}
