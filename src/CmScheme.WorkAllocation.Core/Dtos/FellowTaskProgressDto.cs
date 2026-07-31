namespace CmScheme.WorkAllocation.Core.Dtos;

public sealed class FellowTaskProgressDto
{
    public int TaskProgressId { get; init; }
    public int WorkAllocationId { get; init; }
    public int? UserAccountId { get; init; }
    public string? ProgressNotes { get; init; }
    public int? ProgressPercentage { get; init; }
    public string? FellowProgressStatus { get; init; }
    public DateTime? CreatedOn { get; init; }
}
