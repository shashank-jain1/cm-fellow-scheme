using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Application.Features.ExitInterview.GetExitInterview;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.ExitInterview;

public static class GetExitInterview
{
    public static async Task<IResult> Handle(
        [AsParameters] GetExitInterviewQuery query,
        ISender sender)
    {
        Result<ExitInterviewDto> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
