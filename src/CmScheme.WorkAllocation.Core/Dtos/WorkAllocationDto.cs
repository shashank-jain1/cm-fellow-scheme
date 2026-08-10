namespace CmScheme.WorkAllocation.Core.Dtos;

public sealed class WorkAllocationDto
{
    public int WorkAllocationId { get; init; }
    public int ProjectId { get; init; }
    public string WorkProjectId { get; init; } = null!;
    public string WorkDescription { get; init; } = null!;
    public string Priority { get; init; } = null!;
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int DurationDays { get; init; }
    public int SurveysPerIntern { get; init; }
    public int DivisionId { get; init; }
    public int DistrictId { get; init; }
    public int BlockId { get; init; }
    public bool ActiveStatus { get; init; }
    public string Status { get; init; } = null!;
    public int? AssignedToUserId { get; init; }

    /// <summary>
    /// Rolled up from this allocation's task progress rows:
    /// (completed surveys / assigned surveys) x 100. 0 when nothing is assigned yet.
    /// Settable because it is filled in after the base projection is materialised.
    /// </summary>
    public decimal CompletionPercentage { get; set; }
    public DateTime CreatedOn { get; init; }
    public string CreatedBy { get; init; } = null!;
    public DateTime? ModifiedOn { get; init; }
    public string? ModifiedBy { get; init; }
}
