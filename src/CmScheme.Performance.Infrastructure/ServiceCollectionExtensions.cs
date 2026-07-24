using CmScheme.Performance.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CmScheme.Performance.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPerformanceInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PerformanceDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IPerformanceCommandDbContext>(provider =>
            new PerformanceCommandDbContext(provider.GetRequiredService<PerformanceDbContext>()));

        services.AddScoped<IPerformanceQueryDbContext>(provider =>
            new PerformanceQueryDbContext(provider.GetRequiredService<PerformanceDbContext>()));

        return services;
    }
}
