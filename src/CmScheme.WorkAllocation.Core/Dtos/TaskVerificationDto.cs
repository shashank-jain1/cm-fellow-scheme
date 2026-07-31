namespace CmScheme.WorkAllocation.Core.Dtos;

public sealed class TaskVerificationDto
{
    public int TaskVerificationId { get; init; }
    public int WorkAllocationId { get; init; }
    public int VerifiedBy { get; init; }
    public string VerificationStatus { get; init; } = null!;
    public string? Comments { get; init; }
    public DateTime? VerifiedOn { get; init; }
}
