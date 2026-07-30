using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.HelpDesk.Infrastructure;

public class HelpDeskQueryDbContext : IHelpDeskQueryDbContext
{
    private readonly HelpDeskDbContext _dbContext;

    public HelpDeskQueryDbContext(HelpDeskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Ticket> Tickets => _dbContext.Tickets;
    public IQueryable<TicketActionLog> TicketActionLogs => _dbContext.TicketActionLogs;
    public IQueryable<SlaPolicy> SlaPolicies => _dbContext.SlaPolicies;
    public IQueryable<SlaEscalationLog> SlaEscalationLogs => _dbContext.SlaEscalationLogs;
    public IQueryable<TicketSatisfactionSurvey> TicketSatisfactionSurveys => _dbContext.TicketSatisfactionSurveys;
    public IQueryable<KnowledgeBaseArticle> KnowledgeBaseArticles => _dbContext.KnowledgeBaseArticles;
}
