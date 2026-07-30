using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Performance.Endpoints.ImprovementPlans;

public sealed class ImprovementPlanEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapImprovementPlanEndpoints();
    }
}
