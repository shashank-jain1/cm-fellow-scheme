using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.GetTicketById;

public sealed record GetTicketByIdQuery : IQuery<Result<Core.Dtos.TicketDto?>>
{
    public int TicketId { get; init; }
}
