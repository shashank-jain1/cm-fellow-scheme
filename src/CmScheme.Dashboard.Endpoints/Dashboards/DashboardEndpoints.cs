using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Dashboard.Endpoints.Dashboards;

public sealed class DashboardEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapDashboardEndpoints();
    }
}
