using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.TaskProgress.UpdateTaskProgress;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.TaskProgresses;

public static class Update
{
    public static async Task<IResult> Handle(int taskProgressId, UpdateTaskProgressCommand command, ISender sender)
    {
        UpdateTaskProgressCommand commandWithId = command with { WorkAllocationId = taskProgressId };
        Result<int> result = await sender.Send(commandWithId);
        return result.ToApiResult();
    }
}
