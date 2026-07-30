using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.TaskProgress.UpdateTaskProgress;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.TaskManagement;

public static class UpdateProgress
{
    public static async Task<IResult> Handle(int workAllocationId, UpdateTaskProgressCommand command, ISender sender)
    {
        UpdateTaskProgressCommand commandWithId = command with { WorkAllocationId = workAllocationId };
        Result<int> result = await sender.Send(commandWithId);
        return result.ToApiResult();
    }
}
