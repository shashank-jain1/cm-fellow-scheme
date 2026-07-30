using System.ComponentModel.DataAnnotations.Schema;

namespace CmScheme.WorkAllocation.Core.Entities;

public class WorkAllocation
{
    public int WorkAllocationId { get; set; }
    public int ProjectId { get; set; }
    public string WorkProjectId { get; set; } = null!;
    public string WorkDescription { get; set; } = null!;
    public string Priority { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int DurationDays { get; set; }
    public int SurveysPerIntern { get; set; }
    public int DivisionId { get; set; }
    public int DistrictId { get; set; }
    public int BlockId { get; set; }
    public bool ActiveStatus { get; set; }
    public string Status { get; set; } = null!;
    public int? AssignedToUserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }

    [NotMapped]
    public ICollection<TaskProgress> TaskProgresses { get; set; } = new List<TaskProgress>();

    [NotMapped]
    public ICollection<TaskVerification> TaskVerifications { get; set; } = new List<TaskVerification>();

    [NotMapped]
    public ICollection<TaskAttachment> TaskAttachments { get; set; } = new List<TaskAttachment>();

    [NotMapped]
    public TaskDeadline? Deadline { get; set; }
}
