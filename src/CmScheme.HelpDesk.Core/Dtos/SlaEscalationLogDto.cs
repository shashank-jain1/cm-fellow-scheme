namespace CmScheme.HelpDesk.Core.Dtos;

public sealed class SlaEscalationLogDto
{
    public int EscalationLogId { get; init; }
    public int TicketId { get; init; }
    public int EscalationLevel { get; init; }
    public string EscalatedTo { get; init; } = null!;
    public DateTime EscalatedOn { get; init; }
    public string Reason { get; init; } = null!;
}
