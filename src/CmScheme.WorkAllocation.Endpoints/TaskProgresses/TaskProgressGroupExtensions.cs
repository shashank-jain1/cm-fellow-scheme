using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.WorkAllocation.Endpoints.TaskProgresses;

public static class TaskProgressGroupExtensions
{
    public static IEndpointRouteBuilder MapTaskProgressEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("task-progresses")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.WorkAllocation, "Read", requireScope: false);

        group.MapPost("/", Create.Handle);
        group.MapGet("/{taskProgressId:int}", Get.Handle);
        group.MapGet("/by-work-allocation/{workAllocationId:int}", List.Handle);
        group.MapPut("/{taskProgressId:int}", Update.Handle);
        group.MapPost("/{taskProgressId:int}/survey", RecordSurvey.Handle);

        return builder;
    }
}
