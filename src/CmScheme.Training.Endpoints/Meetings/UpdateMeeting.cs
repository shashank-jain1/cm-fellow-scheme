using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Training.Application.Features.Meeting.UpdateMeeting;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Training.Endpoints.Meetings;

public sealed class UpdateMeeting
{
    public static async Task<IResult> Update(int trainingScheduleId, UpdateMeetingCommand command, ISender sender, CancellationToken ct)
    {
        UpdateMeetingCommand commandWithId = command with { TrainingScheduleId = trainingScheduleId };
        ValueTask<Result> result = sender.Send(commandWithId, ct);
        return await result.ToApiResultAsync();
    }
}
