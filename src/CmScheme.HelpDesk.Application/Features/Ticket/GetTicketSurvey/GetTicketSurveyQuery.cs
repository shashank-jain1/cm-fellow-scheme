using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.GetTicketSurvey;

public sealed record GetTicketSurveyQuery : IQuery<Result<Core.Dtos.TicketSatisfactionSurveyDto?>>
{
    public int TicketId { get; init; }
}
