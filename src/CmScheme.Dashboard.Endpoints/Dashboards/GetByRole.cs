using Ardalis.Result;
using CmScheme.Dashboard.Application.Features.Dashboard.GetDashboardByRole;
using CmScheme.Dashboard.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Dashboard.Endpoints.Dashboards;

public static class GetByRole
{
    public static async Task<IResult> Handle(string role, ISender sender)
    {
        GetDashboardByRoleQuery query = new GetDashboardByRoleQuery { Role = role };
        Result<RoleDashboardDto> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
