using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.WorkAllocation.GetWorkAllocationById;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.WorkAllocations;

public static class Get
{
    public static async Task<IResult> Handle(int workAllocationId, ISender sender)
    {
        GetWorkAllocationByIdQuery query = new GetWorkAllocationByIdQuery { WorkAllocationId = workAllocationId };
        Result<Core.Dtos.WorkAllocationDto?> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
