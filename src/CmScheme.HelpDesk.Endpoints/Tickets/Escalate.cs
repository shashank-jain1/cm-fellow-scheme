using Ardalis.Result;
using CmScheme.HelpDesk.Application.Features.Ticket.EscalateTicket;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class Escalate
{
    public static async Task<IResult> Handle(EscalateTicketCommand command, ISender sender)
    {
        Result result = await sender.Send(command);
        return result.ToApiResult();
    }
}
