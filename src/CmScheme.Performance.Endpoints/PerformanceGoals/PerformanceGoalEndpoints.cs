using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Performance.Endpoints.PerformanceGoals;

public sealed class PerformanceGoalEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapPerformanceGoalEndpoints();
    }
}
