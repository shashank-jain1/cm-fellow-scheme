using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Masters.Endpoints.Works;

public static class WorkGroupExtensions
{
    public static IEndpointRouteBuilder MapWorkGroup(this IEndpointRouteBuilder builder)
    {
        return builder.MapGroup("masters/works")
            .WithDisplayName("Work Masters")
            .WithTags("Masters");
    }
}
