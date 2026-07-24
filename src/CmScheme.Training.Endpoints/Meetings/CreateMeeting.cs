using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CmScheme.Training.Application.Features.Meeting.CreateMeeting;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Training.Endpoints.Meetings;

public sealed class CreateMeeting
{
    public static async Task<IResult> Create(
        [FromBody] CreateMeetingCommand command,
        IMediator mediator,
        CancellationToken ct)
    {
        Result<int> result = await mediator.Send(command, ct);
        return result.ToApiResult();
    }
}
