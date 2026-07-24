using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CmScheme.Training.Core.Data;
using CmScheme.Training.Infrastructure.Data;

namespace CmScheme.Training.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrainingInfrastructure(
        this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDbContext<TrainingCommandDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDbContext<TrainingQueryDbContext>(options =>
            options.UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddScoped<ITrainingCommandDbContext>(provider =>
            provider.GetRequiredService<TrainingCommandDbContext>());

        services.AddScoped<ITrainingQueryDbContext>(provider =>
            provider.GetRequiredService<TrainingQueryDbContext>());

        return services;
    }
}
