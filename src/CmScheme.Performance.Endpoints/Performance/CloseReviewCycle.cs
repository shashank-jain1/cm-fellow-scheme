using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Performance.Application.Features.Performance.ReviewCycle.CloseReviewCycle;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Performance.Endpoints.Performance;

public static class CloseReviewCycle
{
    public static async Task<IResult> Handle(
        int reviewCycleId,
        IMediator mediator,
        CancellationToken ct)
    {
        CloseReviewCycleCommand command = new() { ReviewCycleId = reviewCycleId };
        Result result = await mediator.Send(command, ct);
        return result.ToApiResult();
    }
}
