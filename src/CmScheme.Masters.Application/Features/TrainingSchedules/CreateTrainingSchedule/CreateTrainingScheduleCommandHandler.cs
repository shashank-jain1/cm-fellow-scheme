using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.TrainingSchedules.CreateTrainingSchedule;

public sealed class CreateTrainingScheduleCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<CreateTrainingScheduleCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateTrainingScheduleCommand request,
        CancellationToken cancellationToken)
    {
        TrainingSchedule trainingSchedule = new TrainingSchedule
        {
            CalendarYear = request.CalendarYear,
            ProjectId = request.ProjectId,
            WorkId = request.WorkId,
            DivisionId = request.DivisionId,
            DistrictId = request.DistrictId,
            BlockId = request.BlockId,
            TrainingDate = request.TrainingDate,
            VenueName = request.VenueName,
            TrainingDescription = request.TrainingDescription,
            IsActive = true,
            CreatedOn = DateTime.UtcNow
        };

        dbContext.TrainingSchedules.Add(trainingSchedule);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(trainingSchedule.TrainingScheduleId);
    }
}
