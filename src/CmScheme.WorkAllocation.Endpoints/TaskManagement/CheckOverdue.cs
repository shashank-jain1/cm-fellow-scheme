using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.TaskDeadlines.CheckOverdueTasks;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.TaskManagement;

public static class CheckOverdue
{
    public static async Task<IResult> Handle(ISender sender)
    {
        CheckOverdueTasksCommand command = new CheckOverdueTasksCommand();
        Result<int> result = await sender.Send(command);
        return result.ToApiResult();
    }
}
