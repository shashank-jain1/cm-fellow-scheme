using Microsoft.Extensions.DependencyInjection;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Registration.Application;

namespace CmScheme.Registration.Endpoints;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRegistrationApis(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddApiEndpointsFromAssemblyOf<RegistrationEndpoints>();
        return services;
    }

    public static IServiceCollection AddRegistrationServices(
        this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddRegistrationApplication();
        return services;
    }
}
