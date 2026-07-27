using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.ListTaskProgresses;

public sealed record ListTaskProgressesQuery : IQuery<Result<IReadOnlyList<Core.Dtos.TaskProgressDto>>>
{
    public int WorkAllocationId { get; init; }
}
