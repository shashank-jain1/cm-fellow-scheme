using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Dashboard.Endpoints.Dashboards;

public sealed class DashboardEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapDashboardEndpoints();

        group.MapGet("admin", GetAdmin.Handle)
            .WithTags("Dashboards")
            .WithName("GetAdminDashboard")
            .WithDisplayName("Get admin dashboard");

        group.MapGet("project-progress", GetProjectProgress.Handle)
            .WithTags("Dashboards")
            .WithName("GetProjectProgress")
            .WithDisplayName("Get project progress");

        group.MapGet("by-role/{role}", GetByRole.Handle)
            .WithTags("Dashboards")
            .WithName("GetDashboardByRole")
            .WithDisplayName("Get dashboard by role");

        group.MapGet("fellow/{userId:int}", GetFellow.Handle)
            .WithTags("Dashboards")
            .WithName("GetFellowDashboard")
            .WithDisplayName("Get fellow dashboard");

        group.MapGet("coordinator/{userId:int}", GetCoordinator.Handle)
            .WithTags("Dashboards")
            .WithName("GetCoordinatorDashboard")
            .WithDisplayName("Get coordinator dashboard");

        group.MapPost("export/pdf", ExportPdf.Handle)
            .WithTags("Dashboards")
            .WithName("ExportDashboardPdf")
            .WithDisplayName("Export dashboard as PDF");

        group.MapPost("export/excel", ExportExcel.Handle)
            .WithTags("Dashboards")
            .WithName("ExportDashboardExcel")
            .WithDisplayName("Export dashboard as Excel");
    }
}
