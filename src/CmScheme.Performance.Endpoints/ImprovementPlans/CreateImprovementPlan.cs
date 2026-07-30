using Ardalis.Result;
using Mediator;
using CmScheme.Performance.Application.Features.Performance.ImprovementPlan.CreateImprovementPlan;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Performance.Endpoints.ImprovementPlans;

public static class CreateImprovementPlan
{
    public static async Task<IResult> Handle(
        CreateImprovementPlanCommand command,
        IMediator mediator,
        CancellationToken ct)
    {
        Result<int> result = await mediator.Send(command, ct);
        return result.ToApiResult();
    }
}
