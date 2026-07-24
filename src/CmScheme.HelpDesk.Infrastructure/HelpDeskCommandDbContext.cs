using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.HelpDesk.Infrastructure;

public class HelpDeskCommandDbContext : IHelpDeskCommandDbContext
{
    private readonly HelpDeskDbContext _dbContext;

    public HelpDeskCommandDbContext(HelpDeskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbSet<Ticket> Tickets => _dbContext.Tickets;
    public DbSet<TicketActionLog> TicketActionLogs => _dbContext.TicketActionLogs;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
