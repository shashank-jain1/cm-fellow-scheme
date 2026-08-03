using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Entities;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using TicketEntity = CmScheme.HelpDesk.Core.Entities.Ticket;

namespace CmScheme.HelpDesk.Application.Features.Ticket.CreateTicket;

public sealed class CreateTicketCommandHandler(
    IHelpDeskCommandDbContext helpDeskDbContext,
    IRegistrationCommandDbContext registrationDbContext)
    : ICommandHandler<CreateTicketCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        DateTime now = DateTime.UtcNow;

        string priority = request.Priority ?? Statuses.Priority.Medium;

        if (request.CategoryId.HasValue)
        {
            TicketCategory? category = await registrationDbContext.TicketCategories
                .FirstOrDefaultAsync(tc => tc.TicketCategoryId == request.CategoryId.Value && tc.IsActive, cancellationToken);

            if (category != null && string.IsNullOrEmpty(request.Priority))
            {
                priority = category.DefaultPriority;
            }
        }

        TimeSpan slaWindow = priority switch
        {
            "High" => TimeSpan.FromHours(24),
            "Medium" => TimeSpan.FromHours(72),
            "Low" => TimeSpan.FromHours(168),
            _ => TimeSpan.FromHours(72),
        };

        int? assignedTo = null;

        List<UserAccount> adminUsers = await registrationDbContext.UserAccounts
            .Where(ua => ua.Role == "Admin" && ua.IsActive)
            .OrderBy(ua => ua.UserAccountId)
            .ToListAsync(cancellationToken);

        if (adminUsers.Count > 0)
        {
            TicketEntity? lastTicket = await helpDeskDbContext.Tickets
                .OrderByDescending(t => t.TicketId)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastTicket?.AssignedTo.HasValue != true)
            {
                assignedTo = adminUsers[0].UserAccountId;
            }
            else
            {
                int lastIndex = adminUsers.FindIndex(u => u.UserAccountId == lastTicket.AssignedTo.Value);

                if (lastIndex < 0 || lastIndex >= adminUsers.Count - 1)
                {
                    assignedTo = adminUsers[0].UserAccountId;
                }
                else
                {
                    assignedTo = adminUsers[lastIndex + 1].UserAccountId;
                }
            }
        }

        TicketEntity ticket = new TicketEntity
        {
            ApplicantId = request.ApplicantId,
            Email = request.Email,
            Mobile = request.Mobile,
            IssueCategory = request.IssueCategory,
            IssueDescription = request.IssueDescription,
            Priority = priority,
            Status = Statuses.Ticket.Open,
            CategoryId = request.CategoryId,
            AssignedTo = assignedTo,
            SLADeadline = now.Add(slaWindow),
            SLABreached = false,
            CreatedOn = now
        };

        helpDeskDbContext.Tickets.Add(ticket);
        await helpDeskDbContext.SaveChangesAsync(cancellationToken);

        TicketActionLog actionLog = new TicketActionLog
        {
            TicketId = ticket.TicketId,
            ActionBy = request.Email,
            ActionType = Statuses.TicketAction.Created,
            Remarks = "Ticket created",
            CreatedOn = DateTime.UtcNow
        };

        helpDeskDbContext.TicketActionLogs.Add(actionLog);
        await helpDeskDbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(ticket.TicketId);
    }
}
