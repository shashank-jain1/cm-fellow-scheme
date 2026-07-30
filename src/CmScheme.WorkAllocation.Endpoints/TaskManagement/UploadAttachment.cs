using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.TaskAttachments.UploadTaskAttachment;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.TaskManagement;

public static class UploadAttachment
{
    public static async Task<IResult> Handle(int workAllocationId, IFormFile file, ISender sender)
    {
        UploadTaskAttachmentCommand command = new UploadTaskAttachmentCommand
        {
            WorkAllocationId = workAllocationId,
            File = file
        };
        Result<int> result = await sender.Send(command);
        return result.ToApiResult();
    }
}
