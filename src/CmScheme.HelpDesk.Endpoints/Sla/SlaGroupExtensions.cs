using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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

        group.MapPost("/check-overdue", CheckSlaOverdue.Handle)
            .WithName("CheckSlaOverdue")
            .WithDisplayName("Check SLA overdue breaches")
            .DisableAntiforgery()
            .Produces<int>()
            .ProducesValidationProblem();

        return group;
    }
}
