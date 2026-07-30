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

public sealed record ImprovementPlanDto(
    int ImprovementPlanId,
    int UserAccountId,
    string PlanTitle,
    string? Description,
    DateTime StartDate,
    DateTime EndDate,
    string Status,
    int CreatedBy,
    DateTime CreatedOn);
