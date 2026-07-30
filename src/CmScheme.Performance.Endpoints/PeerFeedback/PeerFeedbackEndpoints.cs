using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Performance.Endpoints.PeerFeedback;

public sealed class PeerFeedbackEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapPeerFeedbackEndpoints();
    }
}
