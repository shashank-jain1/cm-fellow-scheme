using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.TaskProgress.CreateTaskProgress;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.TaskProgresses;

public static class Create
{
    public static async Task<IResult> Handle(CreateTaskProgressCommand command, ISender sender)
    {
        Result<int> result = await sender.Send(command);
        return result.ToApiResult();
    }
}
