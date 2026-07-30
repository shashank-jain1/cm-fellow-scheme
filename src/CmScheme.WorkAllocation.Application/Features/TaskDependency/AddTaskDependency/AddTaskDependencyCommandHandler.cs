using Ardalis.Result;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Entities;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskDependency.AddTaskDependency;

public sealed class AddTaskDependencyCommandHandler(
    IWorkAllocationCommandDbContext dbContext)
    : ICommandHandler<AddTaskDependencyCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        AddTaskDependencyCommand command,
        CancellationToken cancellationToken)
    {
        if (command.WorkAllocationId == command.DependsOnWorkAllocationId)
        {
            return Result<int>.Invalid(new List<ValidationError>
            {
                new() { Identifier = nameof(command.DependsOnWorkAllocationId), ErrorMessage = "A task cannot depend on itself." }
            });
        }

        Core.Entities.TaskDependency dependency = new()
        {
            WorkAllocationId = command.WorkAllocationId,
            DependsOnWorkAllocationId = command.DependsOnWorkAllocationId,
            CreatedOn = DateTime.UtcNow
        };

        dbContext.TaskDependencies.Add(dependency);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(dependency.TaskDependencyId);
    }
}
