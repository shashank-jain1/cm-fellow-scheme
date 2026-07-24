using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.CloseTicket;

public sealed record CloseTicketCommand(int TicketId, string ActionBy, string Remarks) : ICommand<Result>;
