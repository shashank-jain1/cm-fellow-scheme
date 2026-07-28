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
        IQueryable<Core.Entities.Ticket> query = dbContext.Tickets;

        if (request.Role != "Admin" && request.ApplicantId.HasValue)
        {
            query = query.Where(t => t.ApplicantId == request.ApplicantId.Value);
        }

        List<TicketDto> tickets = await query
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
                SLADeadline = t.SLADeadline,
                SLABreached = t.SLADeadline.HasValue && !t.ClosedOn.HasValue && t.SLADeadline.Value < DateTime.UtcNow,
                CreatedOn = t.CreatedOn,
                ClosedOn = t.ClosedOn
            })
            .ToListAsync(cancellationToken);

        return Result<List<TicketDto>>.Success(tickets);
    }
}
