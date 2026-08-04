using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Registration.Endpoints.UserModuleAccess;

public static class UserModuleAccessGroupExtensions
{
    public static IEndpointRouteBuilder MapUserModuleAccessEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/user-module-access")
            .WithTags("User Module Access")
            .RequireAuthorization("AdminPolicy");

        group.MapGet("/", GetAll.Handle)
            .WithName("GetAllModuleAccess")
            .WithDisplayName("Get all users module access");

        group.MapGet("/{userAccountId:int}", GetUserAccess.Handle)
            .WithName("GetUserModuleAccess")
            .WithDisplayName("Get module access for a user");

        group.MapPut("/bulk", BulkUpdate.Handle)
            .WithName("BulkUpdateModuleAccess")
            .WithDisplayName("Bulk update module access for a user");

        group.MapPost("/grant", Grant.Handle)
            .WithName("GrantModuleAccess")
            .WithDisplayName("Grant module access to a user");

        group.MapDelete("/{userModuleAccessId:int}", Revoke.Handle)
            .WithName("RevokeModuleAccess")
            .WithDisplayName("Revoke module access");

        RouteGroupBuilder moduleGroup = builder.MapGroup("/module-master")
            .WithTags("Module Master")
            .RequireAuthorization();

        moduleGroup.MapGet("/", ListModulesEndpoint.Handle)
            .WithName("ListModules")
            .WithDisplayName("List all active modules");

        group.MapGet("/audit-logs", GetAuditLogEndpoint.Handle)
            .WithName("GetAuditLogs")
            .WithDisplayName("Get access audit logs");

        return group;
    }
}
