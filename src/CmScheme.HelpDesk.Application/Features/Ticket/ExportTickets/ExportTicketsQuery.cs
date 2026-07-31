using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.ExportTickets;

public sealed record ExportTicketsQuery : IQuery<Result<byte[]>>
{
    public string? Status { get; init; }
    public string? Priority { get; init; }
}
