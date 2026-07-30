using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.HelpDesk.Application.Features.Sla.CheckSlaBreaches;

public sealed class CheckSlaBreachesCommandHandler(
    IHelpDeskCommandDbContext dbContext,
    INotificationService notificationService)
    : ICommandHandler<CheckSlaBreachesCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(CheckSlaBreachesCommand request, CancellationToken cancellationToken)
    {
        DateTime now = DateTime.UtcNow;
        int escalatedCount = 0;

        List<Core.Entities.Ticket> openTickets = await dbContext.Tickets
            .Where(t => t.Status == Statuses.Ticket.Open || t.Status == Statuses.Ticket.InProgress)
            .ToListAsync(cancellationToken);

        foreach (Core.Entities.Ticket ticket in openTickets)
        {
            SlaPolicy? policy = await dbContext.SlaPolicies
                .FirstOrDefaultAsync(p =>
                    p.TicketCategoryId == ticket.CategoryId &&
                    p.PriorityLevel == ticket.Priority &&
                    p.IsActive, cancellationToken);

            if (policy is null)
            {
                continue;
            }

            List<SlaEscalationLog> existingEscalations = await dbContext.SlaEscalationLogs
                .Where(e => e.TicketId == ticket.TicketId)
                .OrderByDescending(e => e.EscalationLevel)
                .ToListAsync(cancellationToken);

            int currentLevel = existingEscalations.Count;

            if (currentLevel == 0)
            {
                TimeSpan elapsed = now - ticket.CreatedOn;
                if (elapsed.TotalHours > policy.ResponseTimeHours)
                {
                    await CreateEscalationAsync(ticket, policy, 1, "Response time SLA breached", now, cancellationToken);
                    escalatedCount++;
                }
            }
            else
            {
                DateTime lastEscalationOn = existingEscalations[0].EscalatedOn;
                TimeSpan sinceLastEscalation = now - lastEscalationOn;
                if (sinceLastEscalation.TotalHours > policy.ResolutionTimeHours)
                {
                    int nextLevel = currentLevel + 1;
                    await CreateEscalationAsync(ticket, policy, nextLevel, $"Resolution time SLA breached (escalation level {nextLevel})", now, cancellationToken);
                    escalatedCount++;
                }
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return Result<int>.Success(escalatedCount);
    }

    private async Task CreateEscalationAsync(
        Core.Entities.Ticket ticket, SlaPolicy policy, int level, string reason, DateTime now, CancellationToken cancellationToken)
    {
        string[] emails = policy.EscalationEmails
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        string escalatedTo = emails.Length > 0 ? string.Join(",", emails) : "System Admin";

        SlaEscalationLog log = new SlaEscalationLog
        {
            TicketId = ticket.TicketId,
            EscalationLevel = level,
            EscalatedTo = escalatedTo,
            EscalatedOn = now,
            Reason = reason
        };

        dbContext.SlaEscalationLogs.Add(log);

        ticket.SLABreached = true;
        if (ticket.SLADeadline.HasValue && ticket.SLADeadline.Value < now)
        {
            ticket.SLADeadline = now;
        }

        foreach (string email in emails)
        {
            await notificationService.SendEmailAsync(
                email,
                $"SLA Breach - Ticket #{ticket.TicketId}",
                $"Ticket #{ticket.TicketId} has breached SLA. Priority: {ticket.Priority}. Reason: {reason}",
                cancellationToken);
        }

        TicketActionLog actionLog = new TicketActionLog
        {
            TicketId = ticket.TicketId,
            ActionBy = "SLA System",
            ActionType = Statuses.TicketAction.Escalated,
            Remarks = reason,
            CreatedOn = now
        };

        dbContext.TicketActionLogs.Add(actionLog);
    }
}
