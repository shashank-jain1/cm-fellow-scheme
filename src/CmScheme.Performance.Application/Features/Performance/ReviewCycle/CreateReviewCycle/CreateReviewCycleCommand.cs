using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.ReviewCycle.CreateReviewCycle;

public sealed record CreateReviewCycleCommand : ICommand<Result<int>>
{
    public string CycleName { get; init; } = null!;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}
