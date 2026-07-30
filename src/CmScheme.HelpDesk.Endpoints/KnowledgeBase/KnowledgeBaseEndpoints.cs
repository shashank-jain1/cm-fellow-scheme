using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.HelpDesk.Endpoints.KnowledgeBase;

public sealed class KnowledgeBaseEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapKnowledgeBaseEndpoints();
    }
}
