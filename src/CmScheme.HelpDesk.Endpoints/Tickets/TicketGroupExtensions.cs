using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class TicketGroupExtensions
{
    public static IEndpointRouteBuilder MapTicketEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("tickets")
            .RequireAuthorization();

        group.MapPost("/", Create.Handle);
        group.MapPut("/escalate", Escalate.Handle)
            .RequireAuthorization("AdminPolicy");
        group.MapPut("/resolve", Resolve.Handle)
            .RequireAuthorization("AdminPolicy");
        group.MapPut("/close", Close.Handle)
            .RequireAuthorization("AdminPolicy");
        group.MapGet("/{ticketId:int}", Get.Handle);
        group.MapGet("/list", List.Handle);
        group.MapGet("/export", Export.Handle)
            .RequireAuthorization("AdminPolicy");

        return builder;
    }
}
