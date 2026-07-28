using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.TrainingSchedules.UpdateTrainingSchedule;

public sealed class UpdateTrainingScheduleCommandHandler(IMastersCommandDbContext dbContext)
    : ICommandHandler<UpdateTrainingScheduleCommand, Result>
{
    public async ValueTask<Result> Handle(
        UpdateTrainingScheduleCommand request,
        CancellationToken cancellationToken)
    {
        TrainingSchedule? trainingSchedule = await dbContext.TrainingSchedules
            .FirstOrDefaultAsync(ts => ts.TrainingScheduleId == request.TrainingScheduleId, cancellationToken);

        if (trainingSchedule is null)
        {
            return Result.NotFound("Training Schedule not found.");
        }

        trainingSchedule.CalendarYear = request.CalendarYear;
        trainingSchedule.ProjectId = request.ProjectId;
        trainingSchedule.WorkId = request.WorkId;
        trainingSchedule.DivisionId = request.DivisionId;
        trainingSchedule.DistrictId = request.DistrictId;
        trainingSchedule.BlockId = request.BlockId;
        trainingSchedule.TrainingDate = request.TrainingDate;
        trainingSchedule.VenueName = request.VenueName;
        trainingSchedule.TrainingDescription = request.TrainingDescription;
        trainingSchedule.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
