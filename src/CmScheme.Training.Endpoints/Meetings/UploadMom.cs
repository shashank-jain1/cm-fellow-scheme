using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Training.Application.Features.Meeting.UploadMom;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Training.Endpoints.Meetings;

public sealed class UploadMom
{
    public static async Task<IResult> Upload(int trainingScheduleId, IFormFile file, ISender sender, CancellationToken ct)
    {
        UploadMomCommand command = new UploadMomCommand
        {
            TrainingScheduleId = trainingScheduleId,
            FileName = file.FileName,
            FileStream = file.OpenReadStream()
        };
        ValueTask<Result<string>> result = sender.Send(command, ct);
        return await result.ToApiResultAsync();
    }
}
