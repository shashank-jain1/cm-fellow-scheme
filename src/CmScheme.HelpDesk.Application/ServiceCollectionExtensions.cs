using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using CmScheme.HelpDesk.Application.Features.Ticket.CreateTicket;

namespace CmScheme.HelpDesk.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHelpDeskApplication(this IServiceCollection services)
    {
        services.Scan(x => x.FromAssemblyOf<CreateTicketCommandHandler>()
            .AddClasses(filter => filter.Where(t => t.Name.EndsWith("Handler")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(x => x.FromAssemblyOf<CreateTicketCommandHandler>()
            .AddClasses(filter => filter.Where(t => t.Name.EndsWith("Validator")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
