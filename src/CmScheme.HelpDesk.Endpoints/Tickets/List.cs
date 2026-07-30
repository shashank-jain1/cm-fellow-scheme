using Ardalis.Result;
using CmScheme.HelpDesk.Application.Features.Ticket.ListTicketsByRole;
using CmScheme.HelpDesk.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class List
{
    public static async Task<IResult> Handle([AsParameters] ListTicketsByRoleQuery query, ISender sender)
    {
        Result<List<TicketDto>> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
