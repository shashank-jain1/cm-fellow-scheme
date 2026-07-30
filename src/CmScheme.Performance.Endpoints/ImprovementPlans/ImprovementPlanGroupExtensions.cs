using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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

        group.MapPost("/", CreateImprovementPlan.Handle)
            .WithName("CreateImprovementPlan")
            .WithDisplayName("Create improvement plan")
            .WithTags("Improvement Plans")
            .Produces<int>()
            .ProducesValidationProblem();

        group.MapGet("/by-user/{userAccountId:int}", GetImprovementPlans.Handle)
            .WithName("GetImprovementPlans")
            .WithDisplayName("Get improvement plans for a user")
            .WithTags("Improvement Plans")
            .Produces<List<Core.Dtos.ImprovementPlanDto>>();

        return builder;
    }
}
