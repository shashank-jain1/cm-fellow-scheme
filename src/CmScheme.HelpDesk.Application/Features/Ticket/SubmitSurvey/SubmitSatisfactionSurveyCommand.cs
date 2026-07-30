using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.SubmitSurvey;

public sealed record SubmitSatisfactionSurveyCommand : ICommand<Result<int>>
{
    public int TicketId { get; init; }
    public int UserAccountId { get; init; }
    public int Rating { get; init; }
    public string? Comments { get; init; }
}
