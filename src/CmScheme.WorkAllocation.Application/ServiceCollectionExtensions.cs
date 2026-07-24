using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using CmScheme.WorkAllocation.Application.Features.TaskProgress.CreateTaskProgress;

namespace CmScheme.WorkAllocation.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorkAllocationApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.Scan(s => s.FromAssemblyOf<CreateTaskProgressCommandHandler>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}
