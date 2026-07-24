using CmScheme.HelpDesk.Application.Features.Ticket.ListTicketsByRole;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class List
{
    public static async Task<IResult> Handle([AsParameters] ListTicketsByRoleQuery query, ISender sender)
    {
        var result = await sender.Send(query);
        return result.ToApiResult();
    }
}
