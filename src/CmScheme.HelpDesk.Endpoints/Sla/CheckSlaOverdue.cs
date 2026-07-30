using Ardalis.Result;
using CmScheme.HelpDesk.Application.Features.Sla.CheckSlaBreaches;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.HelpDesk.Endpoints.Sla;

public static class CheckSlaOverdue
{
    public static async Task<IResult> Handle(ISender sender)
    {
        CheckSlaBreachesCommand command = new CheckSlaBreachesCommand();
        Result<int> result = await sender.Send(command);
        return result.ToApiResult();
    }
}
