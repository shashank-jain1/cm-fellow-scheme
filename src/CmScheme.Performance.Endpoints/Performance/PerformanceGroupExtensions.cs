using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Performance.Endpoints.Performance;

public static class PerformanceGroupExtensions
{
    public static IEndpointRouteBuilder MapPerformanceEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("performance");

        group.MapGet("/summary", GetSummary.Handle);
        group.MapPut("/rating", RecordRating.Handle);
        group.MapPut("/remarks", RecordRemarks.Handle);
        group.MapGet("/list", List.Handle);

        return builder;
    }
}
