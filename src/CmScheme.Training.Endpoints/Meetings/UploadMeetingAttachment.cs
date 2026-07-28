using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Training.Application.Features.Meeting.UploadMeetingAttachment;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Training.Endpoints.Meetings;

public sealed class UploadMeetingAttachment
{
    public static async Task<IResult> Upload(int trainingScheduleId, IFormFile file, ISender sender, CancellationToken ct)
    {
        UploadMeetingAttachmentCommand command = new UploadMeetingAttachmentCommand
        {
            TrainingScheduleId = trainingScheduleId,
            FileName = file.FileName,
            FileStream = file.OpenReadStream()
        };
        ValueTask<Result<string>> result = sender.Send(command, ct);
        return await result.ToApiResultAsync();
    }
}
