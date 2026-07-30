using Ardalis.Result;
using Mediator;
using CmScheme.Performance.Application.Features.Performance.ImprovementPlan.GetImprovementPlans;
using CmScheme.Performance.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;

namespace CmScheme.Performance.Endpoints.ImprovementPlans;

public static class GetImprovementPlans
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(
        int userAccountId,
        ISender sender)
    {
        GetImprovementPlansQuery query = new() { UserAccountId = userAccountId };
        Result<IReadOnlyList<ImprovementPlanDto>> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
