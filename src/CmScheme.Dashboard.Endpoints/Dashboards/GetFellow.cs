using Ardalis.Result;
using CmScheme.Dashboard.Application.Features.Dashboard.GetFellowDashboard;
using CmScheme.Dashboard.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Dashboard.Endpoints.Dashboards;

public static class GetFellow
{
    public static async Task<IResult> Handle(int fellowId, ISender sender)
    {
        GetFellowDashboardQuery query = new GetFellowDashboardQuery { FellowId = fellowId };
        Result<FellowDashboardDto> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
