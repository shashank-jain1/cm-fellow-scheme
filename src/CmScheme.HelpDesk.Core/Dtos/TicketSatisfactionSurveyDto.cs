namespace CmScheme.HelpDesk.Core.Dtos;

public sealed class TicketSatisfactionSurveyDto
{
    public int SurveyId { get; init; }
    public int TicketId { get; init; }
    public int UserAccountId { get; init; }
    public int Rating { get; init; }
    public string? Comments { get; init; }
    public DateTime SubmittedOn { get; init; }
}
