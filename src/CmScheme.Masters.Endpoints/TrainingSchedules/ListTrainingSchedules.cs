using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.TrainingSchedules.ListTrainingSchedules;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.TrainingSchedules;

public sealed class ListTrainingSchedules
{
    public static async Task<IResult> List(
        string? calendarYear, int? projectId, int? divisionId, ISender sender, CancellationToken ct)
    {
        ValueTask<Result<List<TrainingScheduleDto>>> result = sender.Send(
            new ListTrainingSchedulesQuery
            {
                CalendarYear = calendarYear,
                ProjectId = projectId,
                DivisionId = divisionId
            }, ct);
        return await result.ToApiResultAsync();
    }
}
