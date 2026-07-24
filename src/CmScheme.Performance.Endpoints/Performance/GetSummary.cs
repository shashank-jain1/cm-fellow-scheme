using CmScheme.Performance.Application.Features.Performance.GetPerformanceSummary;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Performance.Endpoints.Performance;

public static class GetSummary
{
    public static async Task<IResult> Handle([AsParameters] GetPerformanceSummaryQuery query, ISender sender)
    {
        var result = await sender.Send(query);
        return result.ToApiResult();
    }
}
