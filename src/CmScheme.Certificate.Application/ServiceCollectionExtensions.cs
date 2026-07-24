using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using CmScheme.Certificate.Application.Features.Certificate.ApplyForCertificate;

namespace CmScheme.Certificate.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCertificateApplication(this IServiceCollection services)
    {
        services.Scan(x => x.FromAssemblyOf<ApplyForCertificateCommandHandler>()
            .AddClasses(filter => filter.Where(t => t.Name.EndsWith("Handler")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(x => x.FromAssemblyOf<ApplyForCertificateCommandHandler>()
            .AddClasses(filter => filter.Where(t => t.Name.EndsWith("Validator")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
