using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public static class TicketGroupExtensions
{
    public static IEndpointRouteBuilder MapTicketEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("tickets");

        group.MapPost("/", Create.Handle);
        group.MapPut("/escalate", Escalate.Handle);
        group.MapPut("/resolve", Resolve.Handle);
        group.MapPut("/close", Close.Handle);
        group.MapGet("/{ticketId:int}", Get.Handle);
        group.MapGet("/list", List.Handle);

        return builder;
    }
}
