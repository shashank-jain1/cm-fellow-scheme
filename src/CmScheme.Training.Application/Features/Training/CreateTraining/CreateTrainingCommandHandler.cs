using Ardalis.Result;
using Mediator;
using CmScheme.Common.Core;
using CmScheme.Training.Core.Data;
using CmScheme.Training.Core.Entities;

namespace CmScheme.Training.Application.Features.Training.CreateTraining;

public sealed class CreateTrainingCommandHandler(
    ITrainingCommandDbContext dbContext)
    : ICommandHandler<CreateTrainingCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateTrainingCommand command,
        CancellationToken cancellationToken)
    {
        TrainingSchedule schedule = new()
        {
            ActivityType = Statuses.Training.TrainingType,
            ProjectId = command.ProjectId,
            WorkProjectId = command.WorkProjectId,
            TrainingTitle = command.TrainingTitle,
            TrainingCategory = command.TrainingCategory,
            TrainingDescription = command.TrainingDescription,
            TargetUserTypes = string.Join(",", command.TargetUserTypes),
            Date = command.Date,
            StartTime = command.StartTime,
            EndTime = command.EndTime,
            Mode = command.Mode,
            ApplicableDivisionIds = string.Join(",", command.ApplicableDivisionIds),
            ApplicableDistrictIds = string.Join(",", command.ApplicableDistrictIds),
            ApplicableBlockIds = string.Join(",", command.ApplicableBlockIds),
            TrainerName = command.TrainerName,
            TrainerMobile = command.TrainerMobile,
            AttendanceRequired = command.AttendanceRequired,
            Remarks = command.Remarks,
            Status = Statuses.Training.Scheduled
        };

        dbContext.TrainingSchedules.Add(schedule);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(schedule.TrainingScheduleId);
    }
}
