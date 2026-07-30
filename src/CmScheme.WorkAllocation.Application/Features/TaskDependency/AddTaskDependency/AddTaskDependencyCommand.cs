using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskDependency.AddTaskDependency;

public sealed record AddTaskDependencyCommand : ICommand<Result<int>>
{
    public int WorkAllocationId { get; init; }
    public int DependsOnWorkAllocationId { get; init; }
}
