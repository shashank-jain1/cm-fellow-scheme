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

        group.MapGet("/", GetTrainingCompletion.Handle);
        group.MapPost("/", CompleteTraining.Handle).DisableAntiforgery();

        return group;
    }
}
