using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions;

namespace CmScheme.Masters.Endpoints.TrainingSchedules;

public sealed class TrainingScheduleEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder group = builder.MapTrainingScheduleEndpoints();

        group.MapGet("/", ListTrainingSchedules.List)
            .WithTags("Training Schedules")
            .WithName("ListTrainingSchedules")
            .WithDisplayName("List all training schedules");

        group.MapGet("/{trainingScheduleId:int}", GetTrainingSchedule.Get)
            .WithTags("Training Schedules")
            .WithName("GetTrainingSchedule")
            .WithDisplayName("Get a training schedule by ID");

        group.MapPost("/", CreateTrainingSchedule.Create)
            .WithTags("Training Schedules")
            .WithName("CreateTrainingSchedule")
            .WithDisplayName("Create a new training schedule");

        group.MapPut("/{trainingScheduleId:int}", UpdateTrainingSchedule.Update)
            .WithTags("Training Schedules")
            .WithName("UpdateTrainingSchedule")
            .WithDisplayName("Update a training schedule");
    }
}
