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

        group.MapGet("/", List.Handle);
        group.MapGet("/summary", GetSummary.Handle);
        group.MapPut("/rating", RecordRating.Handle).DisableAntiforgery();
        group.MapPut("/remarks", RecordRemarks.Handle).DisableAntiforgery();
        group.MapPost("/self-assessment", SubmitSelfAssessment.Handle).DisableAntiforgery();
        group.MapGet("/self-assessment", GetSelfAssessment.Handle);
        group.MapPost("/review-cycle", CreateReviewCycle.Handle).DisableAntiforgery();
        group.MapGet("/review-cycle", GetActiveReviewCycle.Handle);
        group.MapPut("/review-cycle/{reviewCycleId:int}/close", CloseReviewCycle.Handle).DisableAntiforgery();
        group.MapPut("/{performanceEvaluationId:int}/calculate-score", CalculateScore.Handle).DisableAntiforgery();

        return group;
    }
}

public sealed record SubmitReviewRequest(string Action, string PerformedBy, string? Remarks);
