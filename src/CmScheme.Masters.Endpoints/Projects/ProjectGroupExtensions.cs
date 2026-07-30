using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Masters.Endpoints.Projects;

public static class ProjectGroupExtensions
{
    public static IEndpointRouteBuilder MapProjectGroup(this IEndpointRouteBuilder builder)
    {
        return builder.MapGroup("masters/projects")
            .WithDisplayName("Project Masters")
            .WithTags("Masters")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Masters, "Read", requireScope: false);
    }
}
