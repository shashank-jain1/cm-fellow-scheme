using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.DeactivateWorkAllocation;

public sealed record DeactivateWorkAllocationCommand : ICommand<Result>
{
    public int WorkAllocationId { get; init; }
}
