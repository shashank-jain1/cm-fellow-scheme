using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.TaskDependency.AddTaskDependency;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.TaskDependencies;

public static class AddTaskDependency
{
    public static async Task<IResult> Handle(
        AddTaskDependencyCommand command,
        ISender sender)
    {
        Result<int> result = await sender.Send(command);
        return result.ToApiResult();
    }
}
