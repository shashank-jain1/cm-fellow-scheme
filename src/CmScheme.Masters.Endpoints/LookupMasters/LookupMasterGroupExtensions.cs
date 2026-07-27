using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Masters.Endpoints.LookupMasters;

public static class LookupMasterGroupExtensions
{
    public static IEndpointRouteBuilder MapLookupMasterEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/masters/lookup")
            .WithTags("Lookup Masters");

        group.MapGet("/", List.Handle)
            .WithName("ListLookupMasters")
            .WithDisplayName("List lookup masters by type");

        return builder;
    }
}
