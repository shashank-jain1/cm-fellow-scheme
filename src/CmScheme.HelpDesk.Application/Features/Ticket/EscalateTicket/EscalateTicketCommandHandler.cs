using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using TicketEntity = CmScheme.HelpDesk.Core.Entities.Ticket;

using CmScheme.Common.Core.Services;

namespace CmScheme.HelpDesk.Application.Features.Ticket.EscalateTicket;

public sealed class EscalateTicketCommandHandler(
    IHelpDeskCommandDbContext dbContext,
    INotificationService notificationService)
    : ICommandHandler<EscalateTicketCommand, Result>
{
    public async ValueTask<Result> Handle(EscalateTicketCommand request, CancellationToken cancellationToken)
    {
        TicketEntity? ticket = await dbContext.Tickets
            .FirstOrDefaultAsync(t => t.TicketId == request.TicketId, cancellationToken);

        if (ticket is null)
        {
            return Result.NotFound("Ticket not found.");
        }

        ticket.Status = Statuses.Ticket.Escalated;

        TicketActionLog actionLog = new TicketActionLog
        {
            TicketId = request.TicketId,
            ActionBy = request.ActionBy,
            ActionType = Statuses.TicketAction.Escalated,
            Remarks = request.Remarks,
            CreatedOn = DateTime.UtcNow
        };

        dbContext.TicketActionLogs.Add(actionLog);
        await dbContext.SaveChangesAsync(cancellationToken);

        var (subject, body, sms) = NotificationTemplates.TicketEscalated(
            ticket.TicketId,
            ticket.IssueCategory);

        await notificationService.SendEmailAsync(
            ticket.Email,
            subject,
            body,
            cancellationToken);

        await notificationService.SendSmsAsync(
            ticket.Mobile,
            sms,
            cancellationToken);

        return Result.Success();
    }
}
