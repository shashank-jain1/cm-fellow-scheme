using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Certificate.Endpoints.Exit;

public static class ExitGroupExtensions
{
    public static IEndpointRouteBuilder MapExitEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("exit")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Certificate, "Read", requireScope: false);

        return group;
    }
}
