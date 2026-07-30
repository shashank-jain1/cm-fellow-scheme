using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Training.Endpoints.TrainingCompletions;

public sealed class TrainingCompletionEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapTrainingCompletionEndpoints();
    }
}
