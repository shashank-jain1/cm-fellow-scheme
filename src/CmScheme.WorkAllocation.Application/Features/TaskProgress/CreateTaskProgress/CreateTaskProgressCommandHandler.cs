using Ardalis.Result;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Entities;
using Mediator;
using TaskProgressEntity = CmScheme.WorkAllocation.Core.Entities.TaskProgress;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.CreateTaskProgress;

public sealed class CreateTaskProgressCommandHandler(IWorkAllocationCommandDbContext dbContext)
    : ICommandHandler<CreateTaskProgressCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(CreateTaskProgressCommand request, CancellationToken cancellationToken)
    {
        TaskProgressEntity taskProgress = new TaskProgressEntity
        {
            WorkAllocationId = request.WorkAllocationId,
            ProjectName = request.ProjectName,
            WorkProject = request.WorkProject,
            WorkDescription = request.WorkDescription,
            Priority = request.Priority,
            NumberOfSurveys = request.NumberOfSurveys,
            CompletedSurveys = request.CompletedSurveys,
            CompletionDate = request.CompletionDate,
            WorkStatus = request.WorkStatus,
            CompletionPercentage = request.CompletionPercentage
        };

        dbContext.TaskProgresses.Add(taskProgress);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(taskProgress.TaskProgressId);
    }
}
