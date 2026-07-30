using Ardalis.Result;
using CmScheme.HelpDesk.Application.Features.Ticket.GetTicketById;
using CmScheme.HelpDesk.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class Get
{
    public static async Task<IResult> Handle(int ticketId, ISender sender)
    {
        Result<TicketDto?> result = await sender.Send(new GetTicketByIdQuery { TicketId = ticketId });
        return result.ToApiResult();
    }
}
