using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Application.Features.TrainingSchedules.GetTrainingScheduleById;

public sealed class GetTrainingScheduleByIdQueryHandler(IMastersCommandDbContext dbContext)
    : IQueryHandler<GetTrainingScheduleByIdQuery, Result<GetTrainingScheduleByIdResponse>>
{
    public async ValueTask<Result<GetTrainingScheduleByIdResponse>> Handle(
        GetTrainingScheduleByIdQuery request,
        CancellationToken cancellationToken)
    {
        TrainingSchedule? ts = await dbContext.TrainingSchedules
            .FirstOrDefaultAsync(t => t.TrainingScheduleId == request.TrainingScheduleId, cancellationToken);

        if (ts is null)
        {
            return Result.NotFound("Training Schedule not found.");
        }

        return Result.Success(new GetTrainingScheduleByIdResponse
        {
            TrainingScheduleId = ts.TrainingScheduleId,
            CalendarYear = ts.CalendarYear,
            ProjectId = ts.ProjectId,
            WorkId = ts.WorkId,
            DivisionId = ts.DivisionId,
            DistrictId = ts.DistrictId,
            BlockId = ts.BlockId,
            TrainingDate = ts.TrainingDate,
            VenueName = ts.VenueName,
            TrainingDescription = ts.TrainingDescription
        });
    }
}
