using CmScheme.Endpoints.Abstractions;
using CmScheme.Dashboard.Endpoints.Dashboards;
using CmScheme.Dashboard.Application;
using Microsoft.Extensions.DependencyInjection;

namespace CmScheme.Dashboard.Endpoints;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDashboardApis(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddApiEndpointsFromAssemblyOf<DashboardEndpoints>();
        return services;
    }

    public static IServiceCollection AddDashboardServices(
        this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddDashboardApplication();
        return services;
    }
}
