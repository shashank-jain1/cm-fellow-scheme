using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Masters.Endpoints.Locations;

public static class LocationGroupExtensions
{
    public static IEndpointRouteBuilder MapLocationGroup(this IEndpointRouteBuilder builder)
    {
        return builder.MapGroup("masters/locations")
            .WithDisplayName("Location Masters")
            .WithTags("Masters");
    }
}
