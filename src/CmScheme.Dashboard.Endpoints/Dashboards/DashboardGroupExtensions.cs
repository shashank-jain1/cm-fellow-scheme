using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Dashboard.Endpoints.Dashboards;

public static class DashboardGroupExtensions
{
    public static RouteGroupBuilder MapDashboardEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("dashboards")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Dashboard, "Read", requireScope: false);

        return group;
    }
}
