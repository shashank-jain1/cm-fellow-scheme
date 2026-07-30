using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using TicketEntity = CmScheme.HelpDesk.Core.Entities.Ticket;

namespace CmScheme.HelpDesk.Application.Features.Ticket.SubmitSurvey;

public sealed class SubmitSatisfactionSurveyCommandHandler(IHelpDeskCommandDbContext dbContext)
    : ICommandHandler<SubmitSatisfactionSurveyCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(SubmitSatisfactionSurveyCommand request, CancellationToken cancellationToken)
    {
        TicketEntity? ticket = await dbContext.Tickets
            .FirstOrDefaultAsync(t => t.TicketId == request.TicketId, cancellationToken);

        if (ticket is null)
        {
            return Result.NotFound("Ticket not found.");
        }

        if (ticket.Status != Statuses.Ticket.Closed && ticket.Status != Statuses.Ticket.Resolved)
        {
            return Result.Invalid(new ValidationError("Ticket must be closed or resolved before submitting a survey."));
        }

        bool alreadySubmitted = await dbContext.TicketSatisfactionSurveys
            .AnyAsync(s => s.TicketId == request.TicketId && s.UserAccountId == request.UserAccountId, cancellationToken);

        if (alreadySubmitted)
        {
            return Result.Invalid(new ValidationError("You have already submitted a survey for this ticket."));
        }

        TicketSatisfactionSurvey survey = new TicketSatisfactionSurvey
        {
            TicketId = request.TicketId,
            UserAccountId = request.UserAccountId,
            Rating = request.Rating,
            Comments = request.Comments,
            SubmittedOn = DateTime.UtcNow
        };

        dbContext.TicketSatisfactionSurveys.Add(survey);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(survey.SurveyId);
    }
}
