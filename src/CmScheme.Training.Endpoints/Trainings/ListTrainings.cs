using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Training.Application.Features.Training.ListTrainings;
using CmScheme.Training.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Training.Endpoints.Trainings;

public sealed class ListTrainings
{
    public static async Task<IResult> List(
        IMediator mediator,
        CancellationToken ct)
    {
        Result<List<TrainingScheduleDto>> result = await mediator.Send(new ListTrainingsQuery(), ct);
        return result.ToApiResult();
    }
}
