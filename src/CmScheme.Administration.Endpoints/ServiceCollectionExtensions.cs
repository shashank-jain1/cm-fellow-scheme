using CmScheme.Endpoints.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CmScheme.Administration.Endpoints;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdministrationApis(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddApiEndpointsFromAssemblyOf<AdministrationEndpoints>();
        return services;
    }
}
