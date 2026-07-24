using Ardalis.Result;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using TicketEntity = CmScheme.HelpDesk.Core.Entities.Ticket;

namespace CmScheme.HelpDesk.Application.Features.Ticket.ResolveTicket;

public sealed class ResolveTicketCommandHandler(IHelpDeskCommandDbContext dbContext)
    : ICommandHandler<ResolveTicketCommand, Result>
{
    public async ValueTask<Result> Handle(ResolveTicketCommand request, CancellationToken cancellationToken)
    {
        TicketEntity? ticket = await dbContext.Tickets
            .FirstOrDefaultAsync(t => t.TicketId == request.TicketId, cancellationToken);

        if (ticket is null)
        {
            return Result.NotFound("Ticket not found.");
        }

        ticket.Status = "Resolved";
        ticket.ResolutionRemarks = request.ResolutionRemarks;

        TicketActionLog actionLog = new TicketActionLog
        {
            TicketId = request.TicketId,
            ActionBy = request.ActionBy,
            ActionType = "Resolved",
            Remarks = request.ResolutionRemarks,
            CreatedOn = DateTime.UtcNow
        };

        dbContext.TicketActionLogs.Add(actionLog);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
