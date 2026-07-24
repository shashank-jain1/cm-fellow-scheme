using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.WorkAllocation.Endpoints.TaskProgresses;

public sealed class TaskProgressEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapTaskProgressEndpoints();
    }
}
