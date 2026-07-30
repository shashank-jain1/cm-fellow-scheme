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

public sealed class SlaEscalationLogDto
{
    public int EscalationLogId { get; init; }
    public int TicketId { get; init; }
    public int EscalationLevel { get; init; }
    public string EscalatedTo { get; init; } = null!;
    public DateTime EscalatedOn { get; init; }
    public string Reason { get; init; } = null!;
}

public sealed class TicketSatisfactionSurveyDto
{
    public int SurveyId { get; init; }
    public int TicketId { get; init; }
    public int UserAccountId { get; init; }
    public int Rating { get; init; }
    public string? Comments { get; init; }
    public DateTime SubmittedOn { get; init; }
}
