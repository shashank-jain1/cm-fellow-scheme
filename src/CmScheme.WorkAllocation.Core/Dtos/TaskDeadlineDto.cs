namespace CmScheme.WorkAllocation.Core.Dtos;

public sealed class TaskDeadlineDto
{
    public int TaskDeadlineId { get; init; }
    public int WorkAllocationId { get; init; }
    public DateTime DeadlineDate { get; init; }
    public int ReminderDaysBefore { get; init; }
    public bool IsOverdue { get; init; }
    public DateTime? LastReminderSentOn { get; init; }
    public DateTime CreatedOn { get; init; }
}
