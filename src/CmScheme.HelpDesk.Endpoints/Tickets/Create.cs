using Ardalis.Result;
using CmScheme.HelpDesk.Application.Features.Ticket.CreateTicket;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class Create
{
    public static async Task<IResult> Handle(CreateTicketCommand command, ISender sender)
    {
        Result<int> result = await sender.Send(command);
        return result.ToApiResult();
    }
}
