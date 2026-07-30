using Ardalis.Result;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.WorkAllocation.Application.Features.TaskAttachments.GetTaskAttachments;

public sealed class GetTaskAttachmentsQueryHandler(IWorkAllocationQueryDbContext dbContext)
    : IQueryHandler<GetTaskAttachmentsQuery, Result<IReadOnlyList<TaskAttachmentDto>>>
{
    public async ValueTask<Result<IReadOnlyList<TaskAttachmentDto>>> Handle(GetTaskAttachmentsQuery request, CancellationToken cancellationToken)
    {
        List<TaskAttachmentDto> attachments = await dbContext.TaskAttachments
            .Where(ta => ta.WorkAllocationId == request.WorkAllocationId)
            .Select(ta => new TaskAttachmentDto
            {
                TaskAttachmentId = ta.TaskAttachmentId,
                WorkAllocationId = ta.WorkAllocationId,
                UserAccountId = ta.UserAccountId,
                FileName = ta.FileName,
                FilePath = ta.FilePath,
                FileSize = ta.FileSize,
                ContentType = ta.ContentType,
                CreatedOn = ta.CreatedOn
            })
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<TaskAttachmentDto>>.Success(attachments);
    }
}
