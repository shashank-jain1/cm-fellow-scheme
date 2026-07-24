using CmScheme.Certificate.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CmScheme.Certificate.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCertificateInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CertificateDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<ICertificateCommandDbContext>(provider =>
            new CertificateCommandDbContext(provider.GetRequiredService<CertificateDbContext>()));

        services.AddScoped<ICertificateQueryDbContext>(provider =>
            new CertificateQueryDbContext(provider.GetRequiredService<CertificateDbContext>()));

        return services;
    }
}
