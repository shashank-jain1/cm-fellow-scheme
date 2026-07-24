using Microsoft.Extensions.DependencyInjection;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Masters.Application;
using CmScheme.Masters.Endpoints.Locations;

namespace CmScheme.Masters.Endpoints;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMastersApis(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddApiEndpointsFromAssemblyOf<LocationEndpoints>();
        return services;
    }

    public static IServiceCollection AddMastersServices(
        this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddMastersApplication();
        return services;
    }
}
