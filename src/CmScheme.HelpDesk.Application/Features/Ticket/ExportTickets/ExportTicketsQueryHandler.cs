using Ardalis.Result;
using CmScheme.HelpDesk.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.HelpDesk.Application.Features.Ticket.ExportTickets;

public sealed class ExportTicketsQueryHandler(IHelpDeskQueryDbContext dbContext)
    : IQueryHandler<ExportTicketsQuery, Result<byte[]>>
{
    public async ValueTask<Result<byte[]>> Handle(ExportTicketsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.Ticket> query = dbContext.Tickets;

        if (!string.IsNullOrEmpty(request.Status))
        {
            query = query.Where(t => t.Status == request.Status);
        }

        if (!string.IsNullOrEmpty(request.Priority))
        {
            query = query.Where(t => t.Priority == request.Priority);
        }

        List<Core.Entities.Ticket> tickets = await query
            .OrderByDescending(t => t.CreatedOn)
            .ToListAsync(cancellationToken);

        string csv = "TicketId,ApplicantId,Email,Mobile,Category,Description,Priority,Status,SLADeadline,SLABreached,CreatedOn,ClosedOn\n";
        foreach (Core.Entities.Ticket t in tickets)
        {
            string escapedDescription = $"\"{(t.IssueDescription ?? string.Empty).Replace("\"", "\"\"")}\"";
            string slaDeadline = t.SLADeadline?.ToString("yyyy-MM-dd HH:mm") ?? "";
            string closedOn = t.ClosedOn?.ToString("yyyy-MM-dd HH:mm") ?? "";
            csv += $"{t.TicketId},{t.ApplicantId},{t.Email},{t.Mobile},{t.IssueCategory},{escapedDescription},{t.Priority},{t.Status},{slaDeadline},{t.SLABreached},{t.CreatedOn:yyyy-MM-dd HH:mm},{closedOn}\n";
        }

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(csv);
        return Result<byte[]>.Success(bytes);
    }
}
