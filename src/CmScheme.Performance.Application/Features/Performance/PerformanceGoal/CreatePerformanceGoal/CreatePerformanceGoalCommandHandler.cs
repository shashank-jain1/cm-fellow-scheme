using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.Performance.Core.Data;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.PerformanceGoal.CreatePerformanceGoal;

public sealed class CreatePerformanceGoalCommandHandler(
    IPerformanceCommandDbContext dbContext)
    : ICommandHandler<CreatePerformanceGoalCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        CreatePerformanceGoalCommand command,
        CancellationToken cancellationToken)
    {
        Core.Entities.PerformanceGoal goal = new()
        {
            UserAccountId = command.UserAccountId,
            GoalTitle = command.GoalTitle,
            Description = command.Description,
            TargetDate = command.TargetDate,
            Status = Statuses.PerformanceGoal.NotStarted,
            ReviewCycleId = command.ReviewCycleId,
            CreatedOn = DateTime.UtcNow
        };

        dbContext.PerformanceGoals.Add(goal);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(goal.PerformanceGoalId);
    }
}
