using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.HelpDesk.Endpoints.KnowledgeBase;

public static class KnowledgeBaseGroupExtensions
{
    public static IEndpointRouteBuilder MapKnowledgeBaseEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("knowledge-base")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.HelpDesk, "Read", requireScope: false);

        return group;
    }
}
