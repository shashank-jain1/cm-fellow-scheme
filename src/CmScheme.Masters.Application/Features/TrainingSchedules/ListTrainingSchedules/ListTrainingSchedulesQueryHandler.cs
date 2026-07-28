using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;

namespace CmScheme.Masters.Application.Features.TrainingSchedules.ListTrainingSchedules;

public sealed class ListTrainingSchedulesQueryHandler(IMastersCommandDbContext dbContext)
    : IQueryHandler<ListTrainingSchedulesQuery, Result<List<TrainingScheduleDto>>>
{
    public async ValueTask<Result<List<TrainingScheduleDto>>> Handle(
        ListTrainingSchedulesQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.TrainingSchedule> query = dbContext.TrainingSchedules
            .Include(ts => ts.Project);

        if (!string.IsNullOrEmpty(request.CalendarYear))
        {
            query = query.Where(ts => ts.CalendarYear == request.CalendarYear);
        }

        if (request.ProjectId.HasValue)
        {
            query = query.Where(ts => ts.ProjectId == request.ProjectId.Value);
        }

        if (request.DivisionId.HasValue)
        {
            query = query.Where(ts => ts.DivisionId == request.DivisionId.Value);
        }

        List<TrainingScheduleDto> result = await query
            .Select(ts => new TrainingScheduleDto
            {
                TrainingScheduleId = ts.TrainingScheduleId,
                CalendarYear = ts.CalendarYear,
                ProjectId = ts.ProjectId,
                ProjectName = ts.Project.ProjectName,
                WorkId = ts.WorkId,
                DivisionId = ts.DivisionId,
                DistrictId = ts.DistrictId,
                BlockId = ts.BlockId,
                TrainingDate = ts.TrainingDate,
                VenueName = ts.VenueName,
                TrainingDescription = ts.TrainingDescription,
                IsActive = ts.IsActive
            })
            .ToListAsync(cancellationToken);

        return Result.Success(result);
    }
}
