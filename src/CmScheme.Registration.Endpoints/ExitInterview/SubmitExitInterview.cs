using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Application.Features.ExitInterview.SubmitExitInterview;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.ExitInterview;

public static class SubmitExitInterview
{
    public static async Task<IResult> Handle(
        SubmitExitInterviewCommand command,
        IMediator mediator,
        CancellationToken ct)
    {
        Result result = await mediator.Send(command, ct);
        return result.ToApiResult();
    }
}
