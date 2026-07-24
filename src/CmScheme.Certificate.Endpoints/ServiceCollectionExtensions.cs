using Microsoft.Extensions.DependencyInjection;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Certificate.Application;
using CmScheme.Certificate.Endpoints.Certificates;

namespace CmScheme.Certificate.Endpoints;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCertificateApis(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddApiEndpointsFromAssemblyOf<CertificateEndpoints>();
        return services;
    }

    public static IServiceCollection AddCertificateServices(
        this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddCertificateApplication();
        return services;
    }
}
