using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.WorkAllocation.UpdateWorkAllocation;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.WorkAllocations;

public static class Update
{
    public static async Task<IResult> Handle(int workAllocationId, UpdateWorkAllocationCommand command, ISender sender)
    {
        UpdateWorkAllocationCommand commandWithId = command with { WorkAllocationId = workAllocationId };
        Result<bool> result = await sender.Send(commandWithId);
        return result.ToApiResult();
    }
}
