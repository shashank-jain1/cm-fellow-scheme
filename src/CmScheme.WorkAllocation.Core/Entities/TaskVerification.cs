namespace CmScheme.WorkAllocation.Core.Entities;

public class TaskVerification
{
    public int TaskVerificationId { get; set; }
    public int WorkAllocationId { get; set; }
    public int VerifiedBy { get; set; }
    public string VerificationStatus { get; set; } = null!;
    public string? Comments { get; set; }
    public DateTime? VerifiedOn { get; set; }
}
