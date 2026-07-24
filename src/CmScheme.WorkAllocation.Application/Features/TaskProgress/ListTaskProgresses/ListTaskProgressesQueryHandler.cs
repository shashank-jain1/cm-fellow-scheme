using Ardalis.Result;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.ListTaskProgresses;

public sealed class ListTaskProgressesQueryHandler(IWorkAllocationQueryDbContext dbContext)
    : IQueryHandler<ListTaskProgressesQuery, Result<IReadOnlyList<TaskProgressDto>>>
{
    public async ValueTask<Result<IReadOnlyList<TaskProgressDto>>> Handle(ListTaskProgressesQuery request, CancellationToken cancellationToken)
    {
        List<TaskProgressDto> taskProgresses = await dbContext.TaskProgresses
            .Where(t => t.WorkAllocationId == request.WorkAllocationId)
            .Select(t => new TaskProgressDto
            {
                TaskProgressId = t.TaskProgressId,
                WorkAllocationId = t.WorkAllocationId,
                ProjectName = t.ProjectName,
                WorkProject = t.WorkProject,
                WorkDescription = t.WorkDescription,
                Priority = t.Priority,
                NumberOfSurveys = t.NumberOfSurveys,
                CompletedSurveys = t.CompletedSurveys,
                CompletionDate = t.CompletionDate,
                WorkStatus = t.WorkStatus,
                CompletionPercentage = t.CompletionPercentage
            })
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<TaskProgressDto>>.Success(taskProgresses);
    }
}
