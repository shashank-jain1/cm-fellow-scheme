using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.EscalateTicket;

public sealed record EscalateTicketCommand : ICommand<Result>
{
    public int TicketId { get; init; }
    public string ActionBy { get; init; } = null!;
    public string Remarks { get; init; } = null!;
}
