using CmScheme.HelpDesk.Application.Features.Ticket.CreateTicket;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class Create
{
    public static async Task<IResult> Handle(CreateTicketCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
