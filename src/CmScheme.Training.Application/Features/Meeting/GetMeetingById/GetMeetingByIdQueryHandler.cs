using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Training.Core.Data;
using CmScheme.Training.Core.Entities;
using CmScheme.Training.Application.Features.Meeting.CreateMeeting;

namespace CmScheme.Training.Application.Features.Meeting.GetMeetingById;

public sealed class GetMeetingByIdQueryHandler(ITrainingCommandDbContext dbContext)
    : IQueryHandler<GetMeetingByIdQuery, Result<CreateMeetingCommand>>
{
    public async ValueTask<Result<CreateMeetingCommand>> Handle(
        GetMeetingByIdQuery request,
        CancellationToken cancellationToken)
    {
        TrainingSchedule? meeting = await dbContext.TrainingSchedules
            .FirstOrDefaultAsync(t => t.TrainingScheduleId == request.TrainingScheduleId, cancellationToken);

        if (meeting is null)
        {
            return Result.NotFound("Meeting not found.");
        }

        return new CreateMeetingCommand
        {
            MeetingTitle = meeting.MeetingTitle ?? string.Empty,
            MeetingAgenda = meeting.MeetingAgenda,
            MeetingDescription = meeting.MeetingDescription,
            ConductPersonId = meeting.ConductPersonId ?? 0,
            CoordinatorId = meeting.CoordinatorId ?? 0,
            Date = meeting.Date,
            StartTime = meeting.StartTime,
            EndTime = meeting.EndTime,
            MOMRequired = meeting.MOMRequired,
            Remarks = meeting.Remarks
        };
    }
}
