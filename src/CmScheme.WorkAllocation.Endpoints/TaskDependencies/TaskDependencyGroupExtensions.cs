using Microsoft.AspNetCore.Builder;
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

        group.MapPost("/", AddTaskDependency.Handle).DisableAntiforgery();
        group.MapGet("/work-allocation/{workAllocationId:int}", GetTaskDependencies.Handle);

        return group;
    }
}
