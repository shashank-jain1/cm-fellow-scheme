namespace CmScheme.Training.Core.Dtos;

/// <summary>
/// Activity list/calendar row. StartTime, EndTime, Mode and WorkProjectId are part of the
/// contract because the activity views display them.
/// </summary>
public sealed record TrainingScheduleDto(
    int TrainingScheduleId,
    string ActivityType,
    int ProjectId,
    int WorkProjectId,
    string? TrainingTitle,
    string? MeetingTitle,
    DateTime Date,
    DateTime StartTime,
    DateTime EndTime,
    string? Mode,
    string Status);
