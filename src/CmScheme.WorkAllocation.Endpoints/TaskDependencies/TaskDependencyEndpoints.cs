using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.WorkAllocation.Endpoints.TaskDependencies;

public sealed class TaskDependencyEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapTaskDependencyEndpoints();
    }
}
