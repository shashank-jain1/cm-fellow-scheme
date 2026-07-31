namespace CmScheme.Performance.Core.Dtos;

public sealed record PerformanceGoalDto(
    int PerformanceGoalId,
    int UserAccountId,
    string GoalTitle,
    string? Description,
    DateTime TargetDate,
    string Status,
    int? ReviewCycleId,
    DateTime CreatedOn);
