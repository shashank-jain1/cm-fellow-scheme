using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Training.Endpoints.TrainingCompletions;

public static class TrainingCompletionGroupExtensions
{
    public static IEndpointRouteBuilder MapTrainingCompletionEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("training/completions")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Training, "Read", requireScope: false);

        group.MapPost("/", CompleteTraining.Handle)
            .WithName("CompleteTraining")
            .WithDisplayName("Complete a training")
            .WithTags("Training Completions")
            .Produces<int>()
            .ProducesValidationProblem();

        group.MapGet("/", GetTrainingCompletion.Handle)
            .WithName("GetTrainingCompletion")
            .WithDisplayName("Get training completion status")
            .WithTags("Training Completions")
            .Produces<Core.Dtos.TrainingCompletionDto?>();

        return builder;
    }
}
