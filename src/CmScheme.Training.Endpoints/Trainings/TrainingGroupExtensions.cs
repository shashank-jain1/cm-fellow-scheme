using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Training.Endpoints.Trainings;

public static class TrainingGroupExtensions
{
    public static IEndpointRouteBuilder MapTrainingGroup(this IEndpointRouteBuilder builder)
    {
        return builder.MapGroup("training/sessions")
            .WithDisplayName("Training Sessions")
            .WithTags("Training");
    }
}
