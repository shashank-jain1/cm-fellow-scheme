using Ardalis.Result;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using TicketEntity = CmScheme.HelpDesk.Core.Entities.Ticket;

namespace CmScheme.HelpDesk.Application.Features.Ticket.CloseTicket;

public sealed class CloseTicketCommandHandler(IHelpDeskCommandDbContext dbContext)
    : ICommandHandler<CloseTicketCommand, Result>
{
    public async ValueTask<Result> Handle(CloseTicketCommand request, CancellationToken cancellationToken)
    {
        TicketEntity? ticket = await dbContext.Tickets
            .FirstOrDefaultAsync(t => t.TicketId == request.TicketId, cancellationToken);

        if (ticket is null)
        {
            return Result.NotFound("Ticket not found.");
        }

        ticket.Status = "Closed";
        ticket.ClosedOn = DateTime.UtcNow;

        TicketActionLog actionLog = new TicketActionLog
        {
            TicketId = request.TicketId,
            ActionBy = request.ActionBy,
            ActionType = "Closed",
            Remarks = request.Remarks,
            CreatedOn = DateTime.UtcNow
        };

        dbContext.TicketActionLogs.Add(actionLog);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
