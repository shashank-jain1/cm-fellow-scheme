using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using CmScheme.Registration.Application.Features.Registration.SubmitRegistration;

namespace CmScheme.Registration.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRegistrationApplication(this IServiceCollection services)
    {
        services.Scan(x => x.FromAssemblyOf<SubmitRegistrationCommandHandler>()
            .AddClasses(filter => filter.AssignableTo(typeof(Mediator.ICommandHandler<,>)), false)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        services.Scan(x => x.FromAssemblyOf<SubmitRegistrationCommandHandler>()
            .AddClasses(filter => filter.AssignableTo(typeof(Mediator.IQueryHandler<,>)), false)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        services.Scan(x => x.FromAssemblyOf<SubmitRegistrationCommandHandler>()
            .AddClasses(filter => filter.AssignableTo(typeof(FluentValidation.IValidator<>)), false)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        return services;
    }
}
