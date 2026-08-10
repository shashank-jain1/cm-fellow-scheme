using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Performance.Application.Features.Performance.SubmitPerformanceReview;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Performance.Endpoints.Performance;

public static class SubmitReview
{
    public static async Task<IResult> Handle(
        int performanceEvaluationId,
        SubmitReviewRequest request,
        ISender sender,
        CancellationToken ct)
    {
        SubmitPerformanceReviewCommand command = new SubmitPerformanceReviewCommand
        {
            PerformanceEvaluationId = performanceEvaluationId,
            Action = request.Action,
            PerformedBy = request.PerformedBy,
            Remarks = request.Remarks,
        };

        Result result = await sender.Send(command, ct);
        return result.ToApiResult();
    }
}
