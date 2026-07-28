using Ardalis.Result;
using Mediator;

namespace CmScheme.Training.Application.Features.Meeting.UpdateMeeting;

public sealed record UpdateMeetingCommand : ICommand<Result>
{
    public int TrainingScheduleId { get; init; }
    public string MeetingTitle { get; init; } = string.Empty;
    public string? MeetingAgenda { get; init; }
    public string? MeetingDescription { get; init; }
    public int ConductPersonId { get; init; }
    public int CoordinatorId { get; init; }
    public DateTime Date { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public bool MOMRequired { get; init; }
    public string? Remarks { get; init; }
}
