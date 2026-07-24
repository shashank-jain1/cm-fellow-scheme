using CmScheme.Performance.Application.Features.Performance.ListPerformanceByProject;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Performance.Endpoints.Performance;

public static class List
{
    public static async Task<IResult> Handle([AsParameters] ListPerformanceByProjectQuery query, ISender sender)
    {
        var result = await sender.Send(query);
        return result.ToApiResult();
    }
}
