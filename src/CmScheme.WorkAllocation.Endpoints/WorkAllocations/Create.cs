using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.WorkAllocation.CreateWorkAllocation;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.WorkAllocations;

public static class Create
{
    public static async Task<IResult> Handle(CreateWorkAllocationCommand command, ISender sender)
    {
        Result<int> result = await sender.Send(command);
        return result.ToApiResult();
    }
}
