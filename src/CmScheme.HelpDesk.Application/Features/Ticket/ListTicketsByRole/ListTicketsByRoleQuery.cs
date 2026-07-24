using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.ListTicketsByRole;

public sealed record ListTicketsByRoleQuery(string Role) : IQuery<Result<List<Core.Dtos.TicketDto>>>;
