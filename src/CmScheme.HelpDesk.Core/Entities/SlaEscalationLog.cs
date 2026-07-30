namespace CmScheme.HelpDesk.Core.Entities;

public class SlaEscalationLog
{
    public int EscalationLogId { get; set; }
    public int TicketId { get; set; }
    public int EscalationLevel { get; set; }
    public string EscalatedTo { get; set; } = null!;
    public DateTime EscalatedOn { get; set; }
    public string Reason { get; set; } = null!;
}
