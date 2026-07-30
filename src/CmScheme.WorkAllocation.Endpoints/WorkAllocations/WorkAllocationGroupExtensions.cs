using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.WorkAllocation.Endpoints.WorkAllocations;

public static class WorkAllocationGroupExtensions
{
    public static IEndpointRouteBuilder MapWorkAllocationEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("work-allocations")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.WorkAllocation, "Read", requireScope: false);

        group.MapPost("/", Create.Handle);
        group.MapGet("/{workAllocationId:int}", Get.Handle);
        group.MapGet("/list", List.Handle);
        group.MapPut("/{workAllocationId:int}", Update.Handle);
        group.MapPut("/{workAllocationId:int}/assign", Assign.Handle);
        group.MapPut("/{workAllocationId:int}/deactivate", Deactivate.Handle);

        return builder;
    }
}
