using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.EscalateTicket;

public sealed record EscalateTicketCommand(int TicketId, string ActionBy, string Remarks) : ICommand<Result>;
