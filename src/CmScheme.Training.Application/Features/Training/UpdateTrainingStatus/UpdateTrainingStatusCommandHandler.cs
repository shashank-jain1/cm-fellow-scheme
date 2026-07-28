using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core;
using CmScheme.Training.Core.Data;
using CmScheme.Training.Core.Entities;

namespace CmScheme.Training.Application.Features.Training.UpdateTrainingStatus;

public sealed class UpdateTrainingStatusCommandHandler(
    ITrainingCommandDbContext dbContext)
    : ICommandHandler<UpdateTrainingStatusCommand, Result>
{
    private static readonly Dictionary<string, string[]> AllowedTransitions = new(StringComparer.OrdinalIgnoreCase)
    {
        [Statuses.Training.Scheduled] = [Statuses.Training.Ongoing, Statuses.Training.Cancelled],
        [Statuses.Training.Ongoing] = [Statuses.Training.Completed, Statuses.Training.Cancelled],
        [Statuses.Training.Completed] = [Statuses.Training.Closed],
        [Statuses.Training.Closed] = [],
        [Statuses.Training.Cancelled] = [],
    };

    public async ValueTask<Result> Handle(
        UpdateTrainingStatusCommand command,
        CancellationToken cancellationToken)
    {
        TrainingSchedule? schedule = await dbContext.TrainingSchedules
            .FirstOrDefaultAsync(x => x.TrainingScheduleId == command.TrainingScheduleId, cancellationToken);

        if (schedule is null)
        {
            return Result.NotFound("Training schedule not found.");
        }

        if (!AllowedTransitions.TryGetValue(schedule.Status, out string[]? allowed) || !allowed.Contains(command.NewStatus, StringComparer.OrdinalIgnoreCase))
        {
            return Result.Invalid(new ValidationError($"Cannot transition from '{schedule.Status}' to '{command.NewStatus}'."));
        }

        schedule.Status = command.NewStatus;
        schedule.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
