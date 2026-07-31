namespace CmScheme.Performance.Core.Dtos;

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
