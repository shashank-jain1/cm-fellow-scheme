using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Dashboard.Endpoints.Dashboards;

public static class DashboardGroupExtensions
{
    public static IEndpointRouteBuilder MapDashboardEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("dashboards");

        group.MapGet("/admin", GetAdmin.Handle);
        group.MapGet("/coordinator/{coordinatorId:int}", GetCoordinator.Handle);
        group.MapGet("/project-progress", GetProjectProgress.Handle);

        return builder;
    }
}
