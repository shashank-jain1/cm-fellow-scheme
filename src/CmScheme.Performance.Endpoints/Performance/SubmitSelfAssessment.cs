using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Performance.Application.Features.Performance.SelfAssessment.SubmitSelfAssessment;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Performance.Endpoints.Performance;

public static class SubmitSelfAssessment
{
    public static async Task<IResult> Handle(
        SubmitSelfAssessmentCommand command,
        IMediator mediator,
        CancellationToken ct)
    {
        Result result = await mediator.Send(command, ct);
        return result.ToApiResult();
    }
}
