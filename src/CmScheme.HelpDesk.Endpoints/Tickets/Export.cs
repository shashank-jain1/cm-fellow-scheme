using Ardalis.Result;
using CmScheme.HelpDesk.Application.Features.Ticket.ExportTickets;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class Export
{
    public static async Task<IResult> Handle([AsParameters] ExportTicketsQuery query, ISender sender)
    {
        Result<byte[]> result = await sender.Send(query);

        if (!result.IsSuccess)
        {
            return result.ToApiResult();
        }

        return Results.File(result.Value, "text/csv", $"tickets_{DateTime.UtcNow:yyyyMMdd}.csv");
    }
}
