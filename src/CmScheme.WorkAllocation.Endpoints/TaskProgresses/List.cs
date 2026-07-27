using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.TaskProgress.ListTaskProgresses;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.TaskProgresses;

public static class List
{
    public static async Task<IResult> Handle(int workAllocationId, ISender sender)
    {
        ListTaskProgressesQuery query = new ListTaskProgressesQuery { WorkAllocationId = workAllocationId };
        Result<IReadOnlyList<Core.Dtos.TaskProgressDto>> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
