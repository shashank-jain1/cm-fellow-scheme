using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Masters.Endpoints.LookupMasters;

public static class LookupMasterGroupExtensions
{
    public static IEndpointRouteBuilder MapLookupMasterEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/masters/lookup")
            .WithTags("Lookup Masters")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Masters, "Read", requireScope: false);

        group.MapGet("/", List.Handle)
            .WithName("ListLookupMasters")
            .WithDisplayName("List lookup masters by type");

        return builder;
    }
}
