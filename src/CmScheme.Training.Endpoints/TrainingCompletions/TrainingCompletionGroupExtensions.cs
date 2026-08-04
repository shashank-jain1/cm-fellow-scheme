using Microsoft.AspNetCore.Builder;
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

        return group;
    }
}
