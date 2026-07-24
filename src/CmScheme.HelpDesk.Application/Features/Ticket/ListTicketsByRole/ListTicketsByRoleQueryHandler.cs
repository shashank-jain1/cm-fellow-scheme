using Ardalis.Result;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.HelpDesk.Application.Features.Ticket.ListTicketsByRole;

public sealed class ListTicketsByRoleQueryHandler(IHelpDeskQueryDbContext dbContext)
    : IQueryHandler<ListTicketsByRoleQuery, Result<List<TicketDto>>>
{
    public async ValueTask<Result<List<TicketDto>>> Handle(ListTicketsByRoleQuery request, CancellationToken cancellationToken)
    {
        List<TicketDto> tickets = await dbContext.Tickets
            .Select(t => new TicketDto
            {
                TicketId = t.TicketId,
                ApplicantId = t.ApplicantId,
                Email = t.Email,
                Mobile = t.Mobile,
                IssueCategory = t.IssueCategory,
                IssueDescription = t.IssueDescription,
                Priority = t.Priority,
                Status = t.Status,
                ResolutionRemarks = t.ResolutionRemarks,
                CreatedOn = t.CreatedOn,
                ClosedOn = t.ClosedOn
            })
            .ToListAsync(cancellationToken);

        return Result<List<TicketDto>>.Success(tickets);
    }
}
