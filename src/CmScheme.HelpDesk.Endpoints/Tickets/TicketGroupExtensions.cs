using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;
using CmScheme.HelpDesk.Core.Dtos;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class TicketGroupExtensions
{
    public static IEndpointRouteBuilder MapTicketEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("tickets")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.HelpDesk, "Read", requireScope: false);

        group.MapPost("/", Create.Handle)
            .WithName("CreateTicket")
            .WithDisplayName("Create ticket")
            .DisableAntiforgery()
            .Produces<int>()
            .ProducesValidationProblem();

        group.MapGet("/", List.Handle)
            .WithName("ListTickets")
            .WithDisplayName("List tickets")
            .Produces<List<TicketDto>>();

        group.MapGet("/{ticketId:int}", Get.Handle)
            .WithName("GetTicketById")
            .WithDisplayName("Get ticket by ID")
            .Produces<TicketDto>();

        group.MapPut("/resolve", Resolve.Handle)
            .WithName("ResolveTicket")
            .WithDisplayName("Resolve ticket")
            .DisableAntiforgery()
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();

        group.MapPut("/escalate", Escalate.Handle)
            .WithName("EscalateTicket")
            .WithDisplayName("Escalate ticket")
            .DisableAntiforgery()
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();

        group.MapPut("/close", Close.Handle)
            .WithName("CloseTicket")
            .WithDisplayName("Close ticket")
            .DisableAntiforgery()
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();

        group.MapGet("/export", Export.Handle)
            .WithName("ExportTickets")
            .WithDisplayName("Export tickets as CSV");

        return group;
    }
}
