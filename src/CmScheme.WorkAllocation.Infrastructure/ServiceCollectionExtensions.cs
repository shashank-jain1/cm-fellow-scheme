using CmScheme.WorkAllocation.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CmScheme.WorkAllocation.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorkAllocationInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<WorkAllocationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IWorkAllocationCommandDbContext>(provider =>
            new WorkAllocationCommandDbContext(provider.GetRequiredService<WorkAllocationDbContext>()));

        services.AddScoped<IWorkAllocationQueryDbContext>(provider =>
            new WorkAllocationQueryDbContext(provider.GetRequiredService<WorkAllocationDbContext>()));

        return services;
    }
}
