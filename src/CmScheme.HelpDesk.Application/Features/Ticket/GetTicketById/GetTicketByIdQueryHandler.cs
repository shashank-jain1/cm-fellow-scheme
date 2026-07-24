using Ardalis.Result;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.HelpDesk.Application.Features.Ticket.GetTicketById;

public sealed class GetTicketByIdQueryHandler(IHelpDeskQueryDbContext dbContext)
    : IQueryHandler<GetTicketByIdQuery, Result<TicketDto?>>
{
    public async ValueTask<Result<TicketDto?>> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        TicketDto? ticket = await dbContext.Tickets
            .Where(t => t.TicketId == request.TicketId)
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
            .FirstOrDefaultAsync(cancellationToken);

        return Result<TicketDto?>.Success(ticket);
    }
}
