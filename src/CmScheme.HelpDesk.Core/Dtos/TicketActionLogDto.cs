namespace CmScheme.HelpDesk.Core.Dtos;

public sealed class TicketActionLogDto
{
    public int TicketActionLogId { get; init; }
    public int TicketId { get; init; }
    public string ActionBy { get; init; } = null!;
    public string ActionType { get; init; } = null!;
    public string? Remarks { get; init; }
    public DateTime CreatedOn { get; init; }
}
