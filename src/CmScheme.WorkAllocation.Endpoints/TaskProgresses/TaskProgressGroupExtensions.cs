using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.WorkAllocation.Endpoints.TaskProgresses;

public static class TaskProgressGroupExtensions
{
    public static IEndpointRouteBuilder MapTaskProgressEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("task-progresses");

        group.MapPost("/", Create.Handle);
        group.MapGet("/{taskProgressId:int}", Get.Handle);
        group.MapGet("/by-work-allocation/{workAllocationId:int}", List.Handle);
        group.MapPost("/{taskProgressId:int}/survey", RecordSurvey.Handle);

        return builder;
    }
}
