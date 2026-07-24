using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions;

namespace CmScheme.Training.Endpoints.Trainings;

public sealed class TrainingEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder group = builder.MapTrainingGroup();

        group.MapGet("", ListTrainings.List)
            .WithTags("Training")
            .WithName("ListTrainings")
            .WithDisplayName("List training sessions");

        group.MapPost("", CreateTraining.Create)
            .WithTags("Training")
            .WithName("CreateTraining")
            .WithDisplayName("Create a training session");
    }
}
