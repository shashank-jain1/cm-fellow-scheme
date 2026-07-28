using Microsoft.AspNetCore.Http;
using CmScheme.WorkAllocation.Application.Features.WorkAllocation.AssignWorkAllocation;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.WorkAllocations;

public static class Assign
{
    public static async Task<IResult> Handle(int workAllocationId, AssignWorkAllocationCommand command, ISender sender)
    {
        var result = await sender.Send(command with { WorkAllocationId = workAllocationId });
        return result.ToApiResult();
    }
}
