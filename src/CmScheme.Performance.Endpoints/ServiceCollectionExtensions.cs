using Microsoft.Extensions.DependencyInjection;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Performance.Application;
using CmScheme.Performance.Endpoints.Performance;

namespace CmScheme.Performance.Endpoints;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPerformanceApis(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddApiEndpointsFromAssemblyOf<PerformanceEndpoints>();
        return services;
    }

    public static IServiceCollection AddPerformanceServices(
        this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddPerformanceApplication();
        return services;
    }
}
