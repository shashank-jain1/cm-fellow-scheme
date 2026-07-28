using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Training.Core.Data;
using CmScheme.Training.Core.Entities;

namespace CmScheme.Training.Application.Features.Meeting.UpdateMeeting;

public sealed class UpdateMeetingCommandHandler(ITrainingCommandDbContext dbContext)
    : ICommandHandler<UpdateMeetingCommand, Result>
{
    public async ValueTask<Result> Handle(
        UpdateMeetingCommand request,
        CancellationToken cancellationToken)
    {
        TrainingSchedule? meeting = await dbContext.TrainingSchedules
            .FirstOrDefaultAsync(t => t.TrainingScheduleId == request.TrainingScheduleId, cancellationToken);

        if (meeting is null)
        {
            return Result.NotFound("Meeting not found.");
        }

        meeting.MeetingTitle = request.MeetingTitle;
        meeting.MeetingAgenda = request.MeetingAgenda;
        meeting.MeetingDescription = request.MeetingDescription;
        meeting.ConductPersonId = request.ConductPersonId;
        meeting.CoordinatorId = request.CoordinatorId;
        meeting.Date = request.Date;
        meeting.StartTime = request.StartTime;
        meeting.EndTime = request.EndTime;
        meeting.MOMRequired = request.MOMRequired;
        meeting.Remarks = request.Remarks;
        meeting.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
