namespace CmScheme.HelpDesk.Core.Entities;

public class TicketActionLog
{
    public int TicketActionLogId { get; set; }
    public int TicketId { get; set; }
    public string ActionBy { get; set; } = null!;
    public string ActionType { get; set; } = null!;
    public string? Remarks { get; set; }
    public DateTime CreatedOn { get; set; }
}
