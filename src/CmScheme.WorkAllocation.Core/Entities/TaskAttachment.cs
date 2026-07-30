namespace CmScheme.WorkAllocation.Core.Entities;

public class TaskAttachment
{
    public int TaskAttachmentId { get; set; }
    public int WorkAllocationId { get; set; }
    public int UserAccountId { get; set; }
    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public long FileSize { get; set; }
    public string ContentType { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
}
