namespace CmScheme.Training.Core.Dtos;

public sealed record TrainingScheduleDto(int TrainingScheduleId, string ActivityType, int ProjectId, string? TrainingTitle, string? MeetingTitle, DateTime Date, string Status);
