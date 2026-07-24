using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using CmScheme.Training.Application.Features.Training.CreateTraining;

namespace CmScheme.Training.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrainingApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.Scan(s => s.FromAssemblyOf<CreateTrainingCommandHandler>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}
