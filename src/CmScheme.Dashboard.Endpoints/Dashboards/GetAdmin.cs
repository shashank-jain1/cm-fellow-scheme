using Ardalis.Result;
using CmScheme.Dashboard.Application.Features.Dashboard.GetAdminDashboard;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Dashboard.Endpoints.Dashboards;

public static class GetAdmin
{
    public static async Task<IResult> Handle(ISender sender)
    {
        GetAdminDashboardQuery query = new GetAdminDashboardQuery();
        Result<Core.Dtos.AdminDashboardDto> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
