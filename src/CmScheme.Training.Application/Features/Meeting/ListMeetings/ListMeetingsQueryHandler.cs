using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core;
using CmScheme.Training.Core.Data;
using CmScheme.Training.Core.Dtos;

namespace CmScheme.Training.Application.Features.Meeting.ListMeetings;

public sealed class ListMeetingsQueryHandler(
    ITrainingQueryDbContext queryDbContext)
    : IQueryHandler<ListMeetingsQuery, Result<List<TrainingScheduleDto>>>
{
    public async ValueTask<Result<List<TrainingScheduleDto>>> Handle(
        ListMeetingsQuery query,
        CancellationToken cancellationToken)
    {
        List<TrainingScheduleDto> meetings = await queryDbContext.TrainingSchedules
            .AsNoTracking()
            .Where(x => x.ActivityType == Statuses.Training.Meeting)
            .Select(x => new TrainingScheduleDto(
                x.TrainingScheduleId,
                x.ActivityType,
                x.ProjectId,
                x.TrainingTitle,
                x.MeetingTitle,
                x.Date,
                x.Status))
            .ToListAsync(cancellationToken);

        return Result.Success(meetings);
    }
}
