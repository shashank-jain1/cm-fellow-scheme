namespace CmScheme.HelpDesk.Core.Entities;

public class TicketSatisfactionSurvey
{
    public int SurveyId { get; set; }
    public int TicketId { get; set; }
    public int UserAccountId { get; set; }
    public int Rating { get; set; }
    public string? Comments { get; set; }
    public DateTime SubmittedOn { get; set; }
}
