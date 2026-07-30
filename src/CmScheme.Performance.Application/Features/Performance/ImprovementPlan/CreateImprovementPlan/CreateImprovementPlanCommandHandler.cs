using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.Performance.Core.Data;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.ImprovementPlan.CreateImprovementPlan;

public sealed class CreateImprovementPlanCommandHandler(
    IPerformanceCommandDbContext dbContext)
    : ICommandHandler<CreateImprovementPlanCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreateImprovementPlanCommand command,
        CancellationToken cancellationToken)
    {
        Core.Entities.ImprovementPlan plan = new()
        {
            UserAccountId = command.UserAccountId,
            PlanTitle = command.PlanTitle,
            Description = command.Description,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            Status = Statuses.ImprovementPlan.Active,
            CreatedBy = command.CreatedBy,
            CreatedOn = DateTime.UtcNow
        };

        dbContext.ImprovementPlans.Add(plan);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(plan.ImprovementPlanId);
    }
}
