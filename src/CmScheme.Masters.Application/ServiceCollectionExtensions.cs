using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using CmScheme.Masters.Application.Features.Works.ListWorksByProject;

namespace CmScheme.Masters.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMastersApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.Scan(s => s.FromAssemblyOf<ListWorksByProjectQueryHandler>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}
