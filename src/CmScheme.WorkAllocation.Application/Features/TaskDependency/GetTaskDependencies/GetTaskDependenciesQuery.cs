using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskDependency.GetTaskDependencies;

public sealed record GetTaskDependenciesQuery : IQuery<Result<IReadOnlyList<Core.Dtos.TaskDependencyDto>>>
{
    public int WorkAllocationId { get; init; }
}
