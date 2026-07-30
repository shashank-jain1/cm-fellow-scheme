using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Performance.Application.Features.Performance.SelfAssessment.GetSelfAssessment;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Performance.Endpoints.Performance;

public static class GetSelfAssessment
{
    public static async Task<IResult> Handle(
        [AsParameters] GetSelfAssessmentQuery query,
        ISender sender)
    {
        Result<SelfAssessmentDto> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
