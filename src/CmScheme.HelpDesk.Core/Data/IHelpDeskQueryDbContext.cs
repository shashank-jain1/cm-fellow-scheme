using CmScheme.HelpDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.HelpDesk.Core.Data;

public interface IHelpDeskQueryDbContext
{
    IQueryable<Ticket> Tickets { get; }
    IQueryable<TicketActionLog> TicketActionLogs { get; }
}
