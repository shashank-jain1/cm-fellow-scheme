using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace CmScheme.Endpoints.Abstractions;

public static class EndpointsBuilderExtensions
{
    public static IServiceCollection AddApiEndpointsFromAssemblyOf<T>(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Scan(x => x.FromAssemblyOf<T>()
            .AddClasses(filter => filter.AssignableTo<IApiEndpoint>(), false)
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
        );

        return services;
    }

    public static void MapApiEndpoints(this WebApplication app, string baseApi)
    {
        ArgumentNullException.ThrowIfNull(app);

        IEndpointRouteBuilder routeBuilder = app.MapGroup(baseApi);
        IEnumerable<IApiEndpoint> endpointMappers = app.Services.GetServices<IApiEndpoint>();

        foreach (IApiEndpoint apiEndpoint in endpointMappers)
        {
            apiEndpoint.Configure(routeBuilder);
        }
    }
}
