using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.AssignWorkAllocation;

public sealed record AssignWorkAllocationCommand : ICommand<Result>
{
    public int WorkAllocationId { get; init; }
    public int AssignedToUserId { get; init; }
}
