using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.TaskVerification.VerifyTask;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.TaskManagement;

public static class VerifyTask
{
    public static async Task<IResult> Handle(int workAllocationId, VerifyTaskCommand command, ISender sender)
    {
        VerifyTaskCommand commandWithId = command with { WorkAllocationId = workAllocationId };
        Result result = await sender.Send(commandWithId);
        return result.ToApiResult();
    }
}
