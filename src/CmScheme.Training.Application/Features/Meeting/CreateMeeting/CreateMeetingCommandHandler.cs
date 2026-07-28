using Ardalis.Result;
using Mediator;
using CmScheme.Common.Core;
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
            ActivityType = Statuses.Training.Meeting,
            ProjectId = command.ProjectId,
            WorkProjectId = command.WorkProjectId,
            MeetingTitle = command.MeetingTitle,
            MeetingAgenda = command.MeetingAgenda,
            MeetingDescription = command.MeetingDescription,
            ConductPersonId = command.ConductPersonId,
            CoordinatorId = command.CoordinatorId,
            Date = command.Date,
            StartTime = command.StartTime,
            EndTime = command.EndTime,
            Mode = command.Mode,
            ApplicableDivisionIds = string.Join(",", command.ApplicableDivisionIds),
            ApplicableDistrictIds = string.Join(",", command.ApplicableDistrictIds),
            ApplicableBlockIds = string.Join(",", command.ApplicableBlockIds),
            MOMRequired = command.MOMRequired,
            Remarks = command.Remarks,
            Status = Statuses.Training.Scheduled
        };

        dbContext.TrainingSchedules.Add(schedule);
        await dbContext.SaveChangesAsync(cancellationToken);

        if (command.ParticipantIds.Count > 0)
        {
            List<MeetingParticipant> participants = command.ParticipantIds
                .Select(id => new MeetingParticipant
                {
                    TrainingScheduleId = schedule.TrainingScheduleId,
                    ApplicantId = id,
                    CreatedOn = DateTime.UtcNow
                })
                .ToList();

            dbContext.MeetingParticipants.AddRange(participants);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return Result.Success(schedule.TrainingScheduleId);
    }
}
