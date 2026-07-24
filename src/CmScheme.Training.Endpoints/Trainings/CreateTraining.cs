using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CmScheme.Training.Application.Features.Training.CreateTraining;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Training.Endpoints.Trainings;

public sealed class CreateTraining
{
    public static async Task<IResult> Create(
        [FromBody] CreateTrainingCommand command,
        IMediator mediator,
        CancellationToken ct)
    {
        Result<int> result = await mediator.Send(command, ct);
        return result.ToApiResult();
    }
}
