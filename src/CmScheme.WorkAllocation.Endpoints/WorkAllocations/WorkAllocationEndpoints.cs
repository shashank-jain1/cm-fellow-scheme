using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.WorkAllocation.Endpoints.WorkAllocations;

public sealed class WorkAllocationEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapWorkAllocationEndpoints();
    }
}
