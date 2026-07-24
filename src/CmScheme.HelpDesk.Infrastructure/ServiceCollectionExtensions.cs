using CmScheme.HelpDesk.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CmScheme.HelpDesk.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHelpDeskInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<HelpDeskDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IHelpDeskCommandDbContext>(provider =>
            new HelpDeskCommandDbContext(provider.GetRequiredService<HelpDeskDbContext>()));

        services.AddScoped<IHelpDeskQueryDbContext>(provider =>
            new HelpDeskQueryDbContext(provider.GetRequiredService<HelpDeskDbContext>()));

        return services;
    }
}
