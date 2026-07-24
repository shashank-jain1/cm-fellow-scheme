using CmScheme.Endpoints.Abstractions;
using CmScheme.HelpDesk.Application;
using CmScheme.HelpDesk.Endpoints.Tickets;
using Microsoft.Extensions.DependencyInjection;

namespace CmScheme.HelpDesk.Endpoints;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHelpDeskApis(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddApiEndpointsFromAssemblyOf<TicketEndpoints>();
        return services;
    }

    public static IServiceCollection AddHelpDeskServices(
        this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddHelpDeskApplication();
        return services;
    }
}
