using Ardalis.Result;
using CmScheme.HelpDesk.Application.Features.Ticket.SubmitSurvey;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.HelpDesk.Endpoints.Survey;

public static class SubmitSurvey
{
    public static async Task<IResult> Handle(SubmitSatisfactionSurveyCommand command, ISender sender)
    {
        Result<int> result = await sender.Send(command);
        return result.ToApiResult();
    }
}
