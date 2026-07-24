using Ardalis.Result;
using Mediator;
using CmScheme.Training.Core.Data;
using CmScheme.Training.Core.Entities;

namespace CmScheme.Training.Application.Features.Meeting.CreateMeeting;

public sealed class CreateMeetingCommandHandler(
    ITrainingCommandDbContext dbContext)
    : ICommandHandler<CreateMeetingCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateMeetingCommand command,
        CancellationToken cancellationToken)
    {
        TrainingSchedule schedule = new()
        {
            ActivityType = "Meeting",
            MeetingTitle = command.MeetingTitle,
            MeetingAgenda = command.MeetingAgenda,
            MeetingDescription = command.MeetingDescription,
            ConductPersonId = command.ConductPersonId,
            CoordinatorId = command.CoordinatorId,
            Date = command.Date,
            StartTime = command.StartTime,
            EndTime = command.EndTime,
            MOMRequired = command.MOMRequired,
            Remarks = command.Remarks,
            Status = "Scheduled"
        };

        dbContext.TrainingSchedules.Add(schedule);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(schedule.TrainingScheduleId);
    }
}
