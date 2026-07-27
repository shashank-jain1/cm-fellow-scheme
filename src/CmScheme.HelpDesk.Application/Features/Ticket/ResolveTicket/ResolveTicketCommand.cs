using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.ResolveTicket;

public sealed record ResolveTicketCommand : ICommand<Result>
{
    public int TicketId { get; init; }
    public string ActionBy { get; init; } = null!;
    public string ResolutionRemarks { get; init; } = null!;
}
