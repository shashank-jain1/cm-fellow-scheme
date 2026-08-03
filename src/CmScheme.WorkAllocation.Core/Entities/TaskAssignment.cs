namespace CmScheme.WorkAllocation.Core.Entities;

public class TaskAssignment
{
    public int TaskAssignmentId { get; set; }
    public int WorkAllocationId { get; set; }
    public int AssignedToUserId { get; set; }
    public int AssignedByUserId { get; set; }
    public DateTime AssignedOn { get; set; } = DateTime.UtcNow;
    public DateTime? Deadline { get; set; }
    public string Priority { get; set; } = "Medium";
    public string Status { get; set; } = "Assigned";
    public bool IsActive { get; set; } = true;
}
