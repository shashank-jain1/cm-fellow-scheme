using Ardalis.Result;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.GetTaskProgressById;

public sealed class GetTaskProgressByIdQueryHandler(IWorkAllocationQueryDbContext dbContext)
    : IQueryHandler<GetTaskProgressByIdQuery, Result<TaskProgressDto?>>
{
    public async ValueTask<Result<TaskProgressDto?>> Handle(GetTaskProgressByIdQuery request, CancellationToken cancellationToken)
    {
        TaskProgressDto? taskProgress = await dbContext.TaskProgresses
            .Where(t => t.TaskProgressId == request.TaskProgressId)
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
            .FirstOrDefaultAsync(cancellationToken);

        return Result<TaskProgressDto?>.Success(taskProgress);
    }
}
