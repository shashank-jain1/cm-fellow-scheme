using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Training.Application.Features.Meeting.ListMeetings;
using CmScheme.Training.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Training.Endpoints.Meetings;

public sealed class ListMeetings
{
    public static async Task<IResult> List(
        IMediator mediator,
        CancellationToken ct)
    {
        Result<List<TrainingScheduleDto>> result = await mediator.Send(new ListMeetingsQuery(), ct);
        return result.ToApiResult();
    }
}
