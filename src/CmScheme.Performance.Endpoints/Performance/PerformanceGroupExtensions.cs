using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Extensions;
using CmScheme.Performance.Application.Features.Performance.SubmitPerformanceReview;
using CmScheme.Performance.Application.Features.Performance.GetReviewHistory;

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
        group.MapPost("/{performanceEvaluationId:int}/calculate-score", CalculateScore.Handle);

        group.MapPut("/{performanceEvaluationId:int}/review", async (
            int performanceEvaluationId,
            SubmitReviewRequest request,
            IMediator mediator,
            CancellationToken ct) =>
        {
            SubmitPerformanceReviewCommand command = new()
            {
                PerformanceEvaluationId = performanceEvaluationId,
                Action = request.Action,
                PerformedBy = request.PerformedBy,
                Remarks = request.Remarks
            };
            Result result = await mediator.Send(command, ct);
            return result.ToApiResult();
        })
        .WithName("SubmitPerformanceReview")
        .WithDisplayName("Submit or review performance")
        .WithTags("Performance")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet("/{performanceEvaluationId:int}/review-history", async (
            int performanceEvaluationId,
            IMediator mediator,
            CancellationToken ct) =>
        {
            GetReviewHistoryQuery query = new()
            {
                PerformanceEvaluationId = performanceEvaluationId
            };
            Result<List<ReviewHistoryDto>> result = await mediator.Send(query, ct);
            return result.ToApiResult();
        })
        .WithName("GetReviewHistory")
        .WithDisplayName("Get performance review history")
        .WithTags("Performance")
        .Produces<List<ReviewHistoryDto>>()
        .ProducesProblem(StatusCodes.Status404NotFound);

        return builder;
    }
}

public sealed record SubmitReviewRequest(string Action, string PerformedBy, string? Remarks);
