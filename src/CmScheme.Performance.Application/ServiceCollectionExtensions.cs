using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using CmScheme.Performance.Application.Features.Performance.RecordEvaluationRemarks;

namespace CmScheme.Performance.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPerformanceApplication(this IServiceCollection services)
    {
        services.Scan(x => x.FromAssemblyOf<RecordEvaluationRemarksCommandHandler>()
            .AddClasses(filter => filter.Where(t => t.Name.EndsWith("Handler")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(x => x.FromAssemblyOf<RecordEvaluationRemarksCommandHandler>()
            .AddClasses(filter => filter.Where(t => t.Name.EndsWith("Validator")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
