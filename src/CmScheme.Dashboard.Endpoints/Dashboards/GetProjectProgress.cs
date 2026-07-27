using CmScheme.Dashboard.Application.Features.Dashboard.GetProjectProgress;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Dashboard.Endpoints.Dashboards;

public static class GetProjectProgress
{
    public static async Task<IResult> Handle(ISender sender)
    {
        var result = await sender.Send(new GetProjectProgressQuery());
        return result.ToApiResult();
    }
}
