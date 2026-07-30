using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.WorkAllocation.Application.Features.TaskAttachments.UploadTaskAttachment;

public sealed record UploadTaskAttachmentCommand : ICommand<Result<int>>
{
    public int WorkAllocationId { get; init; }
    public IFormFile File { get; init; } = null!;
}
