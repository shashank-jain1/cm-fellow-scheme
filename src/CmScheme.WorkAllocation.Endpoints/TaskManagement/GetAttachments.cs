using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.TaskAttachments.GetTaskAttachments;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.TaskManagement;

public static class GetAttachments
{
    public static async Task<IResult> Handle(int workAllocationId, ISender sender)
    {
        GetTaskAttachmentsQuery query = new GetTaskAttachmentsQuery { WorkAllocationId = workAllocationId };
        Result<IReadOnlyList<Core.Dtos.TaskAttachmentDto>> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
