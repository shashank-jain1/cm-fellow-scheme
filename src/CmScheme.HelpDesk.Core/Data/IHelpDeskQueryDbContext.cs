using CmScheme.HelpDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.HelpDesk.Core.Data;

public interface IHelpDeskQueryDbContext
{
    IQueryable<Ticket> Tickets { get; }
    IQueryable<TicketActionLog> TicketActionLogs { get; }
    IQueryable<SlaPolicy> SlaPolicies { get; }
    IQueryable<SlaEscalationLog> SlaEscalationLogs { get; }
    IQueryable<TicketSatisfactionSurvey> TicketSatisfactionSurveys { get; }
    IQueryable<KnowledgeBaseArticle> KnowledgeBaseArticles { get; }
}
