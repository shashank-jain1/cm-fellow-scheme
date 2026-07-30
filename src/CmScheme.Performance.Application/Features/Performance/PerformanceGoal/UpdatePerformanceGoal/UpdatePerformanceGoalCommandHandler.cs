using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.PerformanceGoal.UpdatePerformanceGoal;

public sealed class UpdatePerformanceGoalCommandHandler(
    IPerformanceCommandDbContext dbContext)
    : ICommandHandler<UpdatePerformanceGoalCommand, Result>
{
    public async ValueTask<Result> Handle(
        UpdatePerformanceGoalCommand command,
        CancellationToken cancellationToken)
    {
        Core.Entities.PerformanceGoal? goal = await dbContext.PerformanceGoals
            .FirstOrDefaultAsync(g => g.PerformanceGoalId == command.PerformanceGoalId, cancellationToken);

        if (goal is null)
        {
            return Result.NotFound("Performance goal not found.");
        }

        goal.Status = command.Status;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
