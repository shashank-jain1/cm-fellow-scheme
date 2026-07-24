using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.HelpDesk.Endpoints.Tickets;

public sealed class TicketEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapTicketEndpoints();
    }
}
