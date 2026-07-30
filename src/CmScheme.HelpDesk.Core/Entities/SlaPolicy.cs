namespace CmScheme.HelpDesk.Core.Entities;

public class SlaPolicy
{
    public int SlaPolicyId { get; set; }
    public int TicketCategoryId { get; set; }
    public string PriorityLevel { get; set; } = null!;
    public int ResponseTimeHours { get; set; }
    public int ResolutionTimeHours { get; set; }
    public string EscalationEmails { get; set; } = null!;
    public bool IsActive { get; set; }
}
