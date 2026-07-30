using Ardalis.Result;
using Mediator;
using CmScheme.Performance.Application.Features.Performance.ReviewCycle.CreateReviewCycle;
using CmScheme.Endpoints.Abstractions.Extensions;

namespace CmScheme.Performance.Endpoints.Performance;

public static class CreateReviewCycle
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(
        CreateReviewCycleCommand command,
        IMediator mediator,
        CancellationToken ct)
    {
        Result<int> result = await mediator.Send(command, ct);
        return result.ToApiResult();
    }
}
