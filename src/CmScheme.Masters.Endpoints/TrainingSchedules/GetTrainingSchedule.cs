using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.TrainingSchedules.GetTrainingScheduleById;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.TrainingSchedules;

public sealed class GetTrainingSchedule
{
    public static async Task<IResult> Get(int trainingScheduleId, ISender sender, CancellationToken ct)
    {
        ValueTask<Result<GetTrainingScheduleByIdResponse>> result = sender.Send(
            new GetTrainingScheduleByIdQuery { TrainingScheduleId = trainingScheduleId }, ct);
        return await result.ToApiResultAsync();
    }
}
