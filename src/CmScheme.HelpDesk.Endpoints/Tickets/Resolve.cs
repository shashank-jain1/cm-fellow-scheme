using CmScheme.HelpDesk.Application.Features.Ticket.ResolveTicket;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class Resolve
{
    public static async Task<IResult> Handle(ResolveTicketCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
