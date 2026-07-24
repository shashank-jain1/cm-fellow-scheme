using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRegistrationInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<RegistrationCommandDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDbContext<RegistrationQueryDbContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions =>
                sqlOptions.EnableRetryOnFailure()));

        services.AddScoped<IRegistrationCommandDbContext>(provider =>
            provider.GetRequiredService<RegistrationCommandDbContext>());

        services.AddScoped<IRegistrationQueryDbContext>(provider =>
            provider.GetRequiredService<RegistrationQueryDbContext>());

        return services;
    }
}
