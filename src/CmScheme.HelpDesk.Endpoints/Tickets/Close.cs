using CmScheme.HelpDesk.Application.Features.Ticket.CloseTicket;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class Close
{
    public static async Task<IResult> Handle(CloseTicketCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
