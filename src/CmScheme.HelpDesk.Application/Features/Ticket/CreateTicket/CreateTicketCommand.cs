using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.Ticket.CreateTicket;

public sealed record CreateTicketCommand(
    int ApplicantId,
    string Email,
    string Mobile,
    string IssueCategory,
    string IssueDescription,
    string Priority
) : ICommand<Result<int>>;
