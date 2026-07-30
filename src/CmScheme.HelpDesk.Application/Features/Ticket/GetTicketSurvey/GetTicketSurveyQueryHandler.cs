using Ardalis.Result;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.HelpDesk.Application.Features.Ticket.GetTicketSurvey;

public sealed class GetTicketSurveyQueryHandler(IHelpDeskQueryDbContext dbContext)
    : IQueryHandler<GetTicketSurveyQuery, Result<TicketSatisfactionSurveyDto?>>
{
    public async ValueTask<Result<TicketSatisfactionSurveyDto?>> Handle(GetTicketSurveyQuery request, CancellationToken cancellationToken)
    {
        TicketSatisfactionSurveyDto? survey = await dbContext.TicketSatisfactionSurveys
            .Where(s => s.TicketId == request.TicketId)
            .Select(s => new TicketSatisfactionSurveyDto
            {
                SurveyId = s.SurveyId,
                TicketId = s.TicketId,
                UserAccountId = s.UserAccountId,
                Rating = s.Rating,
                Comments = s.Comments,
                SubmittedOn = s.SubmittedOn
            })
            .FirstOrDefaultAsync(cancellationToken);

        return Result<TicketSatisfactionSurveyDto?>.Success(survey);
    }
}
