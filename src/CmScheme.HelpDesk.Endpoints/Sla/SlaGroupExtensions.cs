using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.HelpDesk.Endpoints.Sla;

public static class SlaGroupExtensions
{
    public static IEndpointRouteBuilder MapSlaEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("sla")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.HelpDesk, "Read", requireScope: false);

        return group;
    }
}
