using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.GetWorkAllocationById;

public sealed record GetWorkAllocationByIdQuery : IQuery<Result<Core.Dtos.WorkAllocationDto?>>
{
    public int WorkAllocationId { get; init; }
}
