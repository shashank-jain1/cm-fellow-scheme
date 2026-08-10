using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Performance.Application.Features.Performance.GetReviewHistory;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Performance.Endpoints.Performance;

public static class GetReviewHistory
{
    public static async Task<IResult> Handle(
        int performanceEvaluationId,
        ISender sender,
        CancellationToken ct)
    {
        GetReviewHistoryQuery query = new GetReviewHistoryQuery
        {
            PerformanceEvaluationId = performanceEvaluationId,
        };

        Result<List<ReviewHistoryDto>> result = await sender.Send(query, ct);
        return result.ToApiResult();
    }
}
