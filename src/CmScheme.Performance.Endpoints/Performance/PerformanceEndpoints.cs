using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Performance.Endpoints.Performance;

public sealed class PerformanceEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapPerformanceEndpoints();
    }
}
