using Ardalis.Result;
using Mediator;
using CmScheme.Performance.Application.Features.Performance.PerformanceGoal.CreatePerformanceGoal;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Performance.Endpoints.PerformanceGoals;

public static class CreatePerformanceGoal
{
    public static async Task<IResult> Handle(
        CreatePerformanceGoalCommand command,
        IMediator mediator,
        CancellationToken ct)
    {
        Result<int> result = await mediator.Send(command, ct);
        return result.ToApiResult();
    }
}
