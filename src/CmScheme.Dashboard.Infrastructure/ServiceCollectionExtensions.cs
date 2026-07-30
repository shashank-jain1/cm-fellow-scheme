using CmScheme.Dashboard.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CmScheme.Dashboard.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDashboardInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DashboardDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IDashboardCommandDbContext>(provider =>
            new DashboardCommandDbContext(provider.GetRequiredService<DashboardDbContext>()));

        services.AddScoped<IDashboardQueryDbContext>(provider =>
            new DashboardQueryDbContext(provider.GetRequiredService<DashboardDbContext>()));

        return services;
    }
}
