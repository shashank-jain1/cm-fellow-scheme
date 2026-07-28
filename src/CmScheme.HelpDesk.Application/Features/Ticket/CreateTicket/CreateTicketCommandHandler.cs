using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Entities;
using Mediator;
using TicketEntity = CmScheme.HelpDesk.Core.Entities.Ticket;

namespace CmScheme.HelpDesk.Application.Features.Ticket.CreateTicket;

public sealed class CreateTicketCommandHandler(IHelpDeskCommandDbContext dbContext)
    : ICommandHandler<CreateTicketCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        DateTime now = DateTime.UtcNow;
        TimeSpan slaWindow = request.Priority switch
        {
            "High" => TimeSpan.FromHours(24),
            "Medium" => TimeSpan.FromHours(72),
            "Low" => TimeSpan.FromHours(168),
            _ => TimeSpan.FromHours(72),
        };

        TicketEntity ticket = new TicketEntity
        {
            ApplicantId = request.ApplicantId,
            Email = request.Email,
            Mobile = request.Mobile,
            IssueCategory = request.IssueCategory,
            IssueDescription = request.IssueDescription,
            Priority = request.Priority,
            Status = Statuses.Ticket.Open,
            SLADeadline = now.Add(slaWindow),
            SLABreached = false,
            CreatedOn = now
        };

        dbContext.Tickets.Add(ticket);
        await dbContext.SaveChangesAsync(cancellationToken);

        TicketActionLog actionLog = new TicketActionLog
        {
            TicketId = ticket.TicketId,
            ActionBy = request.Email,
            ActionType = Statuses.TicketAction.Created,
            Remarks = "Ticket created",
            CreatedOn = DateTime.UtcNow
        };

        dbContext.TicketActionLogs.Add(actionLog);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(ticket.TicketId);
    }
}
