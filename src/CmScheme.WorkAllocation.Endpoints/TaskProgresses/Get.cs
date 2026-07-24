using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.TaskProgress.GetTaskProgressById;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.TaskProgresses;

public static class Get
{
    public static async Task<IResult> Handle(int taskProgressId, ISender sender)
    {
        GetTaskProgressByIdQuery query = new GetTaskProgressByIdQuery(taskProgressId);
        Result<Core.Dtos.TaskProgressDto?> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
