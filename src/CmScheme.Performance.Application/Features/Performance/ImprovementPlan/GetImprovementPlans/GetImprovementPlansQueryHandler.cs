using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.ImprovementPlan.GetImprovementPlans;

public sealed class GetImprovementPlansQueryHandler(
    IPerformanceQueryDbContext dbContext)
    : IQueryHandler<GetImprovementPlansQuery, Result<IReadOnlyList<ImprovementPlanDto>>>
{
    public async ValueTask<Result<IReadOnlyList<ImprovementPlanDto>>> Handle(
        GetImprovementPlansQuery query,
        CancellationToken cancellationToken)
    {
        List<ImprovementPlanDto> plans = await dbContext.ImprovementPlans
            .AsNoTracking()
            .Where(p => p.UserAccountId == query.UserAccountId)
            .Select(p => new ImprovementPlanDto(
                p.ImprovementPlanId,
                p.UserAccountId,
                p.PlanTitle,
                p.Description,
                p.StartDate,
                p.EndDate,
                p.Status,
                p.CreatedBy,
                p.CreatedOn))
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<ImprovementPlanDto>>.Success(plans);
    }
}
