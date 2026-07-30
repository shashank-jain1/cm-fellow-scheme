using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.ImprovementPlan.CreateImprovementPlan;

public sealed record CreateImprovementPlanCommand : ICommand<Result<int>>
{
    public int UserAccountId { get; init; }
    public string PlanTitle { get; init; } = null!;
    public string? Description { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int CreatedBy { get; init; }
}
