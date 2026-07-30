using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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

        group.MapPost("/", CreatePerformanceGoal.Handle)
            .WithName("CreatePerformanceGoal")
            .WithDisplayName("Create performance goal")
            .WithTags("Performance Goals")
            .Produces<int>()
            .ProducesValidationProblem();

        group.MapGet("/by-user/{userAccountId:int}", GetPerformanceGoals.Handle)
            .WithName("GetPerformanceGoals")
            .WithDisplayName("Get performance goals for a user")
            .WithTags("Performance Goals")
            .Produces<List<Core.Dtos.PerformanceGoalDto>>();

        group.MapPut("/{performanceGoalId:int}/status", UpdatePerformanceGoal.Handle)
            .WithName("UpdatePerformanceGoal")
            .WithDisplayName("Update performance goal status")
            .WithTags("Performance Goals")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound);

        return builder;
    }
}
