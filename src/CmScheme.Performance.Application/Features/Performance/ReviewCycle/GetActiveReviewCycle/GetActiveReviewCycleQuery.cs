using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.ReviewCycle.GetActiveReviewCycle;

public sealed record GetActiveReviewCycleQuery : IQuery<Result<ReviewCycleDto>>
{
}

public sealed record ReviewCycleDto
{
    public int ReviewCycleId { get; init; }
    public string CycleName { get; init; } = null!;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedOn { get; init; }
}
