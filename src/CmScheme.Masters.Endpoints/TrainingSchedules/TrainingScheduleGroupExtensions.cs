using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Masters.Endpoints.TrainingSchedules;

public static class TrainingScheduleGroupExtensions
{
    public static IEndpointRouteBuilder MapTrainingScheduleEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("masters/training-schedules")
            .WithDisplayName("Training Schedule Calendar")
            .WithTags("Masters")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Masters, "Read", requireScope: false);

        group.MapGet("/", ListTrainingSchedules.List);
        group.MapGet("/{trainingScheduleId:int}", GetTrainingSchedule.Get);
        group.MapPost("/", CreateTrainingSchedule.Create);
        group.MapPut("/{trainingScheduleId:int}", UpdateTrainingSchedule.Update);

        return builder;
    }
}
