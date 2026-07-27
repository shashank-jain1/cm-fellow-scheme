using CmScheme.HelpDesk.Application.Features.Ticket.GetTicketById;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class Get
{
    public static async Task<IResult> Handle(int ticketId, ISender sender)
    {
        var result = await sender.Send(new GetTicketByIdQuery { TicketId = ticketId });
        return result.ToApiResult();
    }
}
