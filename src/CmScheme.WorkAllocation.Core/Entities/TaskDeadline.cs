namespace CmScheme.WorkAllocation.Core.Entities;

public class TaskDeadline
{
    public int TaskDeadlineId { get; set; }
    public int WorkAllocationId { get; set; }
    public DateTime DeadlineDate { get; set; }
    public int ReminderDaysBefore { get; set; } = 3;
    public bool IsOverdue { get; set; }
    public DateTime? LastReminderSentOn { get; set; }
    public DateTime CreatedOn { get; set; }
}
