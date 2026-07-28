using Ardalis.Result;
using Mediator;
using CmScheme.Training.Application.Features.Meeting.CreateMeeting;

namespace CmScheme.Training.Application.Features.Meeting.GetMeetingById;

public sealed record GetMeetingByIdQuery : IQuery<Result<CreateMeetingCommand>>
{
    public int TrainingScheduleId { get; init; }
}
