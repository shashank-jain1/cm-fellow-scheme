using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskAttachments.GetTaskAttachments;

public sealed record GetTaskAttachmentsQuery : IQuery<Result<IReadOnlyList<Core.Dtos.TaskAttachmentDto>>>
{
    public int WorkAllocationId { get; init; }
}
