using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.TaskDependency.GetTaskDependencies;
using CmScheme.WorkAllocation.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.TaskDependencies;

public static class GetTaskDependencies
{
    public static async Task<IResult> Handle(
        int workAllocationId,
        ISender sender)
    {
        GetTaskDependenciesQuery query = new() { WorkAllocationId = workAllocationId };
        Result<IReadOnlyList<TaskDependencyDto>> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
