using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Performance.Endpoints.ImprovementPlans;

public static class ImprovementPlanGroupExtensions
{
    public static IEndpointRouteBuilder MapImprovementPlanEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("improvement-plans")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Performance, "Read", requireScope: false);

        group.MapPost("/", CreateImprovementPlan.Handle).DisableAntiforgery();
        group.MapGet("/user/{userAccountId:int}", GetImprovementPlans.Handle);

        return group;
    }
}
