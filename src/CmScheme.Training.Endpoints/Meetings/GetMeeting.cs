using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Training.Application.Features.Meeting.GetMeetingById;
using CmScheme.Training.Application.Features.Meeting.CreateMeeting;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Training.Endpoints.Meetings;

public sealed class GetMeeting
{
    public static async Task<IResult> GetById(int trainingScheduleId, ISender sender, CancellationToken ct)
    {
        GetMeetingByIdQuery query = new GetMeetingByIdQuery { TrainingScheduleId = trainingScheduleId };
        ValueTask<Result<CreateMeetingCommand>> result = sender.Send(query, ct);
        return await result.ToApiResultAsync();
    }
}
