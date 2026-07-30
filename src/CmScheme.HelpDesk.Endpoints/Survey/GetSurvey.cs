using Ardalis.Result;
using CmScheme.HelpDesk.Application.Features.Ticket.GetTicketSurvey;
using CmScheme.HelpDesk.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.HelpDesk.Endpoints.Survey;

public static class GetSurvey
{
    public static async Task<IResult> Handle(int ticketId, ISender sender)
    {
        GetTicketSurveyQuery query = new GetTicketSurveyQuery { TicketId = ticketId };
        Result<TicketSatisfactionSurveyDto?> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
