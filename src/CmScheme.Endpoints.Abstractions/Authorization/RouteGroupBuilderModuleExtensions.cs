using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Endpoints.Abstractions.Authorization;

public static class RouteGroupBuilderModuleExtensions
{
    public static RouteGroupBuilder RequireModule(
        this RouteGroupBuilder builder,
        string moduleCode,
        string permission,
        bool requireScope = true,
        string scopeKey = "id")
    {
        return builder.WithMetadata(
            new RequiresModuleAttribute(moduleCode, permission, requireScope, scopeKey));
    }
}
