using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.WorkAllocation.Endpoints.TaskDependencies;

public static class TaskDependencyGroupExtensions
{
    public static IEndpointRouteBuilder MapTaskDependencyEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("task-dependencies")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.WorkAllocation, "Read", requireScope: false);

        group.MapPost("/", AddTaskDependency.Handle)
            .WithName("AddTaskDependency")
            .WithDisplayName("Add task dependency")
            .WithTags("Task Dependencies")
            .Produces<int>()
            .ProducesValidationProblem();

        group.MapGet("/by-work-allocation/{workAllocationId:int}", GetTaskDependencies.Handle)
            .WithName("GetTaskDependencies")
            .WithDisplayName("Get task dependencies")
            .WithTags("Task Dependencies")
            .Produces<List<Core.Dtos.TaskDependencyDto>>();

        return builder;
    }
}
