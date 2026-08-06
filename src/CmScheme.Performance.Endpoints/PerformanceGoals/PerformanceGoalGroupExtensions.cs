using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Performance.Endpoints.PerformanceGoals;

public static class PerformanceGoalGroupExtensions
{
    public static IEndpointRouteBuilder MapPerformanceGoalEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("performance/goals")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Performance, "Read", requireScope: false);

        group.MapPost("/", CreatePerformanceGoal.Handle).DisableAntiforgery();
        group.MapGet("/user/{userAccountId:int}", GetPerformanceGoals.Handle);
        group.MapPut("/{performanceGoalId:int}/status", UpdatePerformanceGoal.Handle).DisableAntiforgery();

        return group;
    }
}
