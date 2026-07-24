using Microsoft.Extensions.DependencyInjection;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Training.Application;
using CmScheme.Training.Endpoints.Trainings;

namespace CmScheme.Training.Endpoints;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrainingApis(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddApiEndpointsFromAssemblyOf<TrainingEndpoints>();
        return services;
    }

    public static IServiceCollection AddTrainingServices(
        this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddTrainingApplication();
        return services;
    }
}
