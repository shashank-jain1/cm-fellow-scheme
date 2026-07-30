using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Performance.Application.Features.Performance.PerformanceGoal.GetPerformanceGoals;
using CmScheme.Performance.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Performance.Endpoints.PerformanceGoals;

public static class GetPerformanceGoals
{
    public static async Task<IResult> Handle(
        int userAccountId,
        ISender sender)
    {
        GetPerformanceGoalsQuery query = new() { UserAccountId = userAccountId };
        Result<IReadOnlyList<PerformanceGoalDto>> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
