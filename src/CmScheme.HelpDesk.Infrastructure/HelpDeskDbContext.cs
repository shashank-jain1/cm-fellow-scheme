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
    public DbSet<SlaPolicy> SlaPolicies => Set<SlaPolicy>();
    public DbSet<SlaEscalationLog> SlaEscalationLogs => Set<SlaEscalationLog>();
    public DbSet<TicketSatisfactionSurvey> TicketSatisfactionSurveys => Set<TicketSatisfactionSurvey>();
    public DbSet<KnowledgeBaseArticle> KnowledgeBaseArticles => Set<KnowledgeBaseArticle>();

    public HelpDeskDbContext(DbContextOptions<HelpDeskDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new TicketActionLogConfiguration());
        modelBuilder.ApplyConfiguration(new SlaPolicyConfiguration());
        modelBuilder.ApplyConfiguration(new SlaEscalationLogConfiguration());
        modelBuilder.ApplyConfiguration(new TicketSatisfactionSurveyConfiguration());
        modelBuilder.ApplyConfiguration(new KnowledgeBaseArticleConfiguration());
    }

    IQueryable<Ticket> IHelpDeskQueryDbContext.Tickets => Tickets;
    IQueryable<TicketActionLog> IHelpDeskQueryDbContext.TicketActionLogs => TicketActionLogs;
    IQueryable<SlaPolicy> IHelpDeskQueryDbContext.SlaPolicies => SlaPolicies;
    IQueryable<SlaEscalationLog> IHelpDeskQueryDbContext.SlaEscalationLogs => SlaEscalationLogs;
    IQueryable<TicketSatisfactionSurvey> IHelpDeskQueryDbContext.TicketSatisfactionSurveys => TicketSatisfactionSurveys;
    IQueryable<KnowledgeBaseArticle> IHelpDeskQueryDbContext.KnowledgeBaseArticles => KnowledgeBaseArticles;
}
