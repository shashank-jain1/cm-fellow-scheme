using CmScheme.HelpDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.HelpDesk.Core.Data;

public interface IHelpDeskCommandDbContext
{
    DbSet<Ticket> Tickets { get; }
    DbSet<TicketActionLog> TicketActionLogs { get; }
    DbSet<SlaPolicy> SlaPolicies { get; }
    DbSet<SlaEscalationLog> SlaEscalationLogs { get; }
    DbSet<TicketSatisfactionSurvey> TicketSatisfactionSurveys { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
