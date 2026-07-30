using Ardalis.Result;
using Mediator;
using CmScheme.Performance.Application.Features.Performance.ReviewCycle.GetActiveReviewCycle;
using CmScheme.Endpoints.Abstractions.Extensions;

namespace CmScheme.Performance.Endpoints.Performance;

public static class GetActiveReviewCycle
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(
        ISender sender,
        CancellationToken ct)
    {
        GetActiveReviewCycleQuery query = new();
        Result<ReviewCycleDto> result = await sender.Send(query, ct);
        return result.ToApiResult();
    }
}
