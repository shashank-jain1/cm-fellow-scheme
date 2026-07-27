using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.ListTicketsByRole;

public sealed record ListTicketsByRoleQuery : IQuery<Result<List<Core.Dtos.TicketDto>>>
{
    public string Role { get; init; } = null!;
}
