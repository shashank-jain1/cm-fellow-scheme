using Microsoft.AspNetCore.Http;
using CmScheme.WorkAllocation.Application.Features.WorkAllocation.DeactivateWorkAllocation;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.WorkAllocations;

public static class Deactivate
{
    public static async Task<IResult> Handle(int workAllocationId, ISender sender)
    {
        var result = await sender.Send(new DeactivateWorkAllocationCommand { WorkAllocationId = workAllocationId });
        return result.ToApiResult();
    }
}
