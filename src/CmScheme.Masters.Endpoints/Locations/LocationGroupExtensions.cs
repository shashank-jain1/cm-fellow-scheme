using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Masters.Endpoints.Locations;

public static class LocationGroupExtensions
{
    public static IEndpointRouteBuilder MapLocationGroup(this IEndpointRouteBuilder builder)
    {
        return builder.MapGroup("masters/locations")
            .WithDisplayName("Location Masters")
            .WithTags("Masters")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Masters, "Read", requireScope: false);
    }
}
