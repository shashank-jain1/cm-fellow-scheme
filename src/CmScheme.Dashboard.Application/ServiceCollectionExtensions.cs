using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using CmScheme.Dashboard.Application.Features.Dashboard.GetCoordinatorDashboard;

namespace CmScheme.Dashboard.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDashboardApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.Scan(s => s.FromAssemblyOf<GetCoordinatorDashboardQueryHandler>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}
