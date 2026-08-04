using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using TicketEntity = CmScheme.HelpDesk.Core.Entities.Ticket;

using CmScheme.Common.Core.Services;

namespace CmScheme.HelpDesk.Application.Features.Ticket.CloseTicket;

public sealed class CloseTicketCommandHandler(
    IHelpDeskCommandDbContext dbContext,
    INotificationService notificationService)
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

        ticket.Status = Statuses.Ticket.Closed;
        ticket.ClosedOn = DateTime.UtcNow;

        TicketActionLog actionLog = new TicketActionLog
        {
            TicketId = request.TicketId,
            ActionBy = request.ActionBy,
            ActionType = Statuses.TicketAction.Closed,
            Remarks = request.Remarks,
            CreatedOn = DateTime.UtcNow
        };

        TicketSatisfactionSurvey survey = new TicketSatisfactionSurvey
        {
            TicketId = request.TicketId,
            UserAccountId = ticket.ApplicantId,
            Rating = 0,
            Comments = "Pending",
            SubmittedOn = DateTime.UtcNow
        };

        dbContext.TicketActionLogs.Add(actionLog);
        dbContext.TicketSatisfactionSurveys.Add(survey);
        await dbContext.SaveChangesAsync(cancellationToken);

        var (subject, body, sms) = NotificationTemplates.TicketClosed(
            ticket.TicketId,
            request.Remarks ?? "Resolved & Closed");

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
