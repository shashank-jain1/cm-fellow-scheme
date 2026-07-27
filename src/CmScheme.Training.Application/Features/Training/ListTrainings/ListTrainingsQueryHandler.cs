using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core;
using CmScheme.Training.Core.Data;
using CmScheme.Training.Core.Dtos;

namespace CmScheme.Training.Application.Features.Training.ListTrainings;

public sealed class ListTrainingsQueryHandler(
    ITrainingQueryDbContext queryDbContext)
    : IQueryHandler<ListTrainingsQuery, Result<List<TrainingScheduleDto>>>
{
    public async ValueTask<Result<List<TrainingScheduleDto>>> Handle(
        ListTrainingsQuery query,
        CancellationToken cancellationToken)
    {
        List<TrainingScheduleDto> trainings = await queryDbContext.TrainingSchedules
            .AsNoTracking()
            .Where(x => x.ActivityType == Statuses.Training.TrainingType)
            .Select(x => new TrainingScheduleDto(
                x.TrainingScheduleId,
                x.ActivityType,
                x.ProjectId,
                x.TrainingTitle,
                x.MeetingTitle,
                x.Date,
                x.Status))
            .ToListAsync(cancellationToken);

        return Result.Success(trainings);
    }
}
