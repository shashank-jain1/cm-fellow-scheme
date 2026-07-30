using Ardalis.Result;
using CmScheme.Common.Core.Services;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using TaskAttachmentEntity = CmScheme.WorkAllocation.Core.Entities.TaskAttachment;

namespace CmScheme.WorkAllocation.Application.Features.TaskAttachments.UploadTaskAttachment;

public sealed class UploadTaskAttachmentCommandHandler(
    IWorkAllocationCommandDbContext dbContext,
    ICurrentUserService currentUserService,
    IFileUploadService fileUploadService)
    : ICommandHandler<UploadTaskAttachmentCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(UploadTaskAttachmentCommand request, CancellationToken cancellationToken)
    {
        Core.Entities.WorkAllocation? workAllocation = await dbContext.WorkAllocations
            .FirstOrDefaultAsync(w => w.WorkAllocationId == request.WorkAllocationId, cancellationToken);

        if (workAllocation is null)
        {
            return Result<int>.NotFound("Work allocation not found.");
        }

        int? currentUserId = currentUserService.UserAccountId;
        if (currentUserId is null)
        {
            return Result<int>.Unauthorized("User is not authenticated.");
        }

        Stream fileStream = request.File.OpenReadStream();
        string filePath = await fileUploadService.UploadAsync(
            fileStream,
            request.File.FileName,
            request.File.ContentType,
            "task-attachments",
            cancellationToken);

        TaskAttachmentEntity taskAttachment = new TaskAttachmentEntity
        {
            WorkAllocationId = request.WorkAllocationId,
            UserAccountId = currentUserId.Value,
            FileName = request.File.FileName,
            FilePath = filePath,
            FileSize = request.File.Length,
            ContentType = request.File.ContentType,
            CreatedOn = DateTime.UtcNow
        };

        dbContext.TaskAttachments.Add(taskAttachment);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(taskAttachment.TaskAttachmentId);
    }
}
