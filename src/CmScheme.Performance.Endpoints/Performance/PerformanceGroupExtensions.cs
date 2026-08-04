using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Performance.Endpoints.Performance;

public static class PerformanceGroupExtensions
{
    public static IEndpointRouteBuilder MapPerformanceEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("performance")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Performance, "Read", requireScope: false);

        return group;
    }
}

public sealed record SubmitReviewRequest(string Action, string PerformedBy, string? Remarks);
