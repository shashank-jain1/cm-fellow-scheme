using Ardalis.Result;
using CmScheme.HelpDesk.Application.Features.Ticket.ResolveTicket;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class Resolve
{
    public static async Task<IResult> Handle(ResolveTicketCommand command, ISender sender)
    {
        Result result = await sender.Send(command);
        return result.ToApiResult();
    }
}
