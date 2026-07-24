using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.GetTicketById;

public sealed record GetTicketByIdQuery(int TicketId) : IQuery<Result<Core.Dtos.TicketDto?>>;
