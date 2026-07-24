using CmScheme.Common.Core.Data;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Data.Configurations;
using CmScheme.HelpDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.HelpDesk.Infrastructure;

public class HelpDeskDbContext : BaseDbContext, IHelpDeskCommandDbContext, IHelpDeskQueryDbContext
{
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<TicketActionLog> TicketActionLogs => Set<TicketActionLog>();

    public HelpDeskDbContext(DbContextOptions<HelpDeskDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new TicketActionLogConfiguration());
    }

    IQueryable<Ticket> IHelpDeskQueryDbContext.Tickets => Tickets;
    IQueryable<TicketActionLog> IHelpDeskQueryDbContext.TicketActionLogs => TicketActionLogs;
}
