using Ardalis.Result;
using Mediator;
using CmScheme.Training.Application.Features.Training.CompleteTraining;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Training.Endpoints.TrainingCompletions;

public static class CompleteTraining
{
    public static async Task<IResult> Handle(
        CompleteTrainingCommand command,
        IMediator mediator,
        CancellationToken ct)
    {
        Result<int> result = await mediator.Send(command, ct);
        return result.ToApiResult();
    }
}
