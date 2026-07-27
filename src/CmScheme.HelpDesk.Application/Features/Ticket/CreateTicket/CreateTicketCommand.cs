using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.CreateTicket;

public sealed record CreateTicketCommand : ICommand<Result<int>>
{
    public int ApplicantId { get; init; }
    public string Email { get; init; } = null!;
    public string Mobile { get; init; } = null!;
    public string IssueCategory { get; init; } = null!;
    public string IssueDescription { get; init; } = null!;
    public string Priority { get; init; } = null!;
}
