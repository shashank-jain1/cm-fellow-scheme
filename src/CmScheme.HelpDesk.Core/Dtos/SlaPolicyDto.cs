namespace CmScheme.HelpDesk.Core.Dtos;

public sealed class SlaPolicyDto
{
    public int SlaPolicyId { get; init; }
    public int TicketCategoryId { get; init; }
    public string PriorityLevel { get; init; } = null!;
    public int ResponseTimeHours { get; init; }
    public int ResolutionTimeHours { get; init; }
    public string EscalationEmails { get; init; } = null!;
    public bool IsActive { get; init; }
}
