using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Performance.Application.Features.Performance.PerformanceGoal.UpdatePerformanceGoal;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Performance.Endpoints.PerformanceGoals;

public static class UpdatePerformanceGoal
{
    public static async Task<IResult> Handle(
        int performanceGoalId,
        UpdatePerformanceGoalRequest request,
        IMediator mediator,
        CancellationToken ct)
    {
        UpdatePerformanceGoalCommand command = new()
        {
            PerformanceGoalId = performanceGoalId,
            Status = request.Status
        };
        Result result = await mediator.Send(command, ct);
        return result.ToApiResult();
    }
}

public sealed record UpdatePerformanceGoalRequest(string Status);
