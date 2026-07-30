using Ardalis.Result;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.WorkAllocation.Application.Features.TaskDependency.GetTaskDependencies;

public sealed class GetTaskDependenciesQueryHandler(
    IWorkAllocationQueryDbContext dbContext)
    : IQueryHandler<GetTaskDependenciesQuery, Result<IReadOnlyList<TaskDependencyDto>>>
{
    public async ValueTask<Result<IReadOnlyList<TaskDependencyDto>>> Handle(
        GetTaskDependenciesQuery query,
        CancellationToken cancellationToken)
    {
        List<TaskDependencyDto> dependencies = await dbContext.TaskDependencies
            .AsNoTracking()
            .Where(td => td.WorkAllocationId == query.WorkAllocationId)
            .Select(td => new TaskDependencyDto(
                td.TaskDependencyId,
                td.WorkAllocationId,
                td.DependsOnWorkAllocationId,
                td.CreatedOn))
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<TaskDependencyDto>>.Success(dependencies);
    }
}
