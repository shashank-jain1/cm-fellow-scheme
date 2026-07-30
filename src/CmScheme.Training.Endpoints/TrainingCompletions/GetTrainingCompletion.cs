using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Training.Application.Features.Training.GetTrainingCompletion;
using CmScheme.Training.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Training.Endpoints.TrainingCompletions;

public static class GetTrainingCompletion
{
    public static async Task<IResult> Handle(
        [AsParameters] GetTrainingCompletionQuery query,
        IMediator mediator,
        CancellationToken ct)
    {
        Result<TrainingCompletionDto?> result = await mediator.Send(query, ct);
        return result.ToApiResult();
    }
}
