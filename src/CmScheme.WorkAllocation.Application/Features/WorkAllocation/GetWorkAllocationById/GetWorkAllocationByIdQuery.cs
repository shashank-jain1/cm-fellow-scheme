using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.GetWorkAllocationById;

public sealed record GetWorkAllocationByIdQuery(int WorkAllocationId) : IQuery<Result<Core.Dtos.WorkAllocationDto?>>;
