using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Dashboard.Endpoints.Dashboards;

public static class DashboardGroupExtensions
{
    public static IEndpointRouteBuilder MapDashboardEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("dashboards")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Dashboard, "Read", requireScope: false);

        group.MapGet("/admin", GetAdmin.Handle);
        group.MapGet("/coordinator/{coordinatorId:int}", GetCoordinator.Handle);
        group.MapGet("/fellow/{fellowId:int}", GetFellow.Handle);
        group.MapGet("/by-role/{role}", GetByRole.Handle);
        group.MapGet("/project-progress", GetProjectProgress.Handle);
        group.MapPost("/export/pdf", ExportPdf.Handle);
        group.MapPost("/export/excel", ExportExcel.Handle);

        return builder;
    }
}
