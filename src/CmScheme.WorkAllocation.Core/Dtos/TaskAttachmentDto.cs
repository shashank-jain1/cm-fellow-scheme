namespace CmScheme.WorkAllocation.Core.Dtos;

public sealed class TaskAttachmentDto
{
    public int TaskAttachmentId { get; init; }
    public int WorkAllocationId { get; init; }
    public int UserAccountId { get; init; }
    public string FileName { get; init; } = null!;
    public string FilePath { get; init; } = null!;
    public long FileSize { get; init; }
    public string ContentType { get; init; } = null!;
    public DateTime CreatedOn { get; init; }
}
