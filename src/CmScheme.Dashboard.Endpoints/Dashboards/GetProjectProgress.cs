using Ardalis.Result;
using CmScheme.Dashboard.Application.Features.Dashboard.GetProjectProgress;
using CmScheme.Dashboard.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Dashboard.Endpoints.Dashboards;

public static class GetProjectProgress
{
    public static async Task<IResult> Handle(ISender sender)
    {
        Result<List<ProjectProgressDto>> result = await sender.Send(new GetProjectProgressQuery());
        return result.ToApiResult();
    }
}
