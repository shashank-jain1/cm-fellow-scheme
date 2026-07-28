namespace CmScheme.HelpDesk.Core.Entities;

public class Ticket
{
    public int TicketId { get; set; }
    public int ApplicantId { get; set; }
    public string Email { get; set; } = null!;
    public string Mobile { get; set; } = null!;
    public string IssueCategory { get; set; } = null!;
    public string IssueDescription { get; set; } = null!;
    public string Priority { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string? ResolutionRemarks { get; set; }
    public DateTime? SLADeadline { get; set; }
    public bool SLABreached { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ClosedOn { get; set; }
}
