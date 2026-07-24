using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.ResolveTicket;

public sealed record ResolveTicketCommand(int TicketId, string ActionBy, string ResolutionRemarks) : ICommand<Result>;
