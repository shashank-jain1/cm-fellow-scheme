namespace CmScheme.HelpDesk.Core.Dtos;

public sealed class TicketDto
{
    public int TicketId { get; init; }
    public int ApplicantId { get; init; }
    public string Email { get; init; } = null!;
    public string Mobile { get; init; } = null!;
    public string IssueCategory { get; init; } = null!;
    public string IssueDescription { get; init; } = null!;
    public string Priority { get; init; } = null!;
    public string Status { get; init; } = null!;
    public string? ResolutionRemarks { get; init; }
    public DateTime CreatedOn { get; init; }
    public DateTime? ClosedOn { get; init; }
}

public sealed class TicketActionLogDto
{
    public int TicketActionLogId { get; init; }
    public int TicketId { get; init; }
    public string ActionBy { get; init; } = null!;
    public string ActionType { get; init; } = null!;
    public string? Remarks { get; init; }
    public DateTime CreatedOn { get; init; }
}
