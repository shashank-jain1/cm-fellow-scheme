using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions;

namespace CmScheme.Masters.Endpoints.LookupMasters;

public sealed class LookupMasterEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/masters/lookup")
            .WithTags("Lookup Masters");

        group.MapGet("", List.Handle)
            .WithName("ListLookupMasters")
            .WithDisplayName("List lookup masters by type");
    }
}
