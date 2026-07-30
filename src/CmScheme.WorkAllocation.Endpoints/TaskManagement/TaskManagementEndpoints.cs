using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.WorkAllocation.Endpoints.TaskManagement;

public sealed class TaskManagementEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapTaskManagementEndpoints();
    }
}
