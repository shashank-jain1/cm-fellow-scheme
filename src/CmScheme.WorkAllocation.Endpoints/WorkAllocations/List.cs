using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.WorkAllocation.ListWorkAllocations;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.WorkAllocations;

public static class List
{
    public static async Task<IResult> Handle(ISender sender)
    {
        ListWorkAllocationsQuery query = new ListWorkAllocationsQuery();
        Result<IReadOnlyList<Core.Dtos.WorkAllocationDto>> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
