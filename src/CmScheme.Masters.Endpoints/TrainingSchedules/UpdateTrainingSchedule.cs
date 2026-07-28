using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.TrainingSchedules.UpdateTrainingSchedule;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.TrainingSchedules;

public sealed class UpdateTrainingSchedule
{
    public static async Task<IResult> Update(
        int trainingScheduleId, UpdateTrainingScheduleCommand command, ISender sender, CancellationToken ct)
    {
        UpdateTrainingScheduleCommand updated = command with { TrainingScheduleId = trainingScheduleId };
        ValueTask<Result> result = sender.Send(updated, ct);
        return await result.ToApiResultAsync();
    }
}
