using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.ListWorkAllocations;

public sealed record ListWorkAllocationsQuery : IQuery<Result<IReadOnlyList<Core.Dtos.WorkAllocationDto>>>;
