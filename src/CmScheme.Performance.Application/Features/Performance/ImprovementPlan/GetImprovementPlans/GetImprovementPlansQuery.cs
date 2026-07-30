using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.ImprovementPlan.GetImprovementPlans;

public sealed record GetImprovementPlansQuery : IQuery<Result<IReadOnlyList<Core.Dtos.ImprovementPlanDto>>>
{
    public int UserAccountId { get; init; }
}
