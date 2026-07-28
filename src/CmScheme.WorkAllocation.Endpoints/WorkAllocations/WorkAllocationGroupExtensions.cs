using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.WorkAllocation.Endpoints.WorkAllocations;

public static class WorkAllocationGroupExtensions
{
    public static IEndpointRouteBuilder MapWorkAllocationEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("work-allocations");

        group.MapPost("/", Create.Handle);
        group.MapGet("/{workAllocationId:int}", Get.Handle);
        group.MapGet("/list", List.Handle);
        group.MapPut("/{workAllocationId:int}", Update.Handle);
        group.MapPut("/{workAllocationId:int}/assign", Assign.Handle);
        group.MapPut("/{workAllocationId:int}/deactivate", Deactivate.Handle);

        return builder;
    }
}
