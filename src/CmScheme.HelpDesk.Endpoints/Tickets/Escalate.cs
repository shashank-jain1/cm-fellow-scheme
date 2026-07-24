using CmScheme.HelpDesk.Application.Features.Ticket.EscalateTicket;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class Escalate
{
    public static async Task<IResult> Handle(EscalateTicketCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
