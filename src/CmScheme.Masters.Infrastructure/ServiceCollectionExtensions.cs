using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Infrastructure.Data;

namespace CmScheme.Masters.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMastersInfrastructure(
        this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDbContext<MastersCommandDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDbContext<MastersQueryDbContext>(options =>
            options.UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddScoped<IMastersCommandDbContext>(provider =>
            provider.GetRequiredService<MastersCommandDbContext>());

        services.AddScoped<IMastersQueryDbContext>(provider =>
            provider.GetRequiredService<MastersQueryDbContext>());

        return services;
    }
}
