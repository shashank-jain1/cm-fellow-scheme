using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using CmScheme.Endpoints.Abstractions.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Api.Authorization;

public sealed class ModuleAuthorizationHandler : AuthorizationHandler<ModuleRequirement>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IServiceScopeFactory _scopeFactory;

    public ModuleAuthorizationHandler(
        ICurrentUserService currentUserService,
        IServiceScopeFactory scopeFactory)
    {
        _currentUserService = currentUserService;
        _scopeFactory = scopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ModuleRequirement requirement)
    {
        if (!_currentUserService.UserAccountId.HasValue)
        {
            context.Fail(new AuthorizationFailureReason(this, "User not authenticated."));
            return;
        }

        if (_currentUserService.IsAdmin)
        {
            context.Succeed(requirement);
            return;
        }

        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IRegistrationCommandDbContext>();

        var userAccess = await dbContext.UserModuleAccesses
            .Where(uma =>
                uma.UserAccountId == _currentUserService.UserAccountId.Value &&
                uma.ModuleMaster.ModuleCode == requirement.ModuleCode &&
                uma.IsActive)
            .ToListAsync();

        if (userAccess.Count == 0)
        {
            context.Fail(new AuthorizationFailureReason(this,
                $"No access to module '{requirement.ModuleCode}'."));
            return;
        }

        var hasPermission = requirement.Permission switch
        {
            "Read" => userAccess.Any(a => a.CanRead),
            "Write" => userAccess.Any(a => a.CanWrite),
            "Approve" => userAccess.Any(a => a.CanApprove),
            "Export" => userAccess.Any(a => a.CanExport),
            _ => false
        };

        if (!hasPermission)
        {
            context.Fail(new AuthorizationFailureReason(this,
                $"Insufficient permissions for '{requirement.Permission}' in module '{requirement.ModuleCode}'."));
            return;
        }

        if (requirement.RequireScope)
        {
            var scopeKey = requirement.ScopeKey;
            var locationId = ExtractLocationId(context, scopeKey);

            if (locationId.HasValue)
            {
                var userRole = _currentUserService.Role;
                bool scoped = userRole switch
                {
                    "Coordinator" => userAccess.Any(a =>
                        a.DivisionId != null &&
                        IsLocationWithinScope(locationId.Value, a.DivisionId, a.DistrictId, a.BlockId)),
                    "Fellow" => userAccess.Any(a =>
                        a.DistrictId != null &&
                        locationId.Value == a.DistrictId),
                    "Intern" => userAccess.Any(a =>
                        a.BlockId != null &&
                        locationId.Value == a.BlockId),
                    _ => true
                };

                if (!scoped)
                {
                    context.Fail(new AuthorizationFailureReason(this,
                        "Location scope denied for this operation."));
                    return;
                }
            }
        }

        context.Succeed(requirement);
    }

    private static int? ExtractLocationId(AuthorizationHandlerContext context, string scopeKey)
    {
        var httpContext = context.Resource switch
        {
            Microsoft.AspNetCore.Http.DefaultHttpContext httpCtx => httpCtx,
            Microsoft.AspNetCore.Mvc.ActionContext actionCtx => actionCtx.HttpContext,
            _ => null
        };

        if (httpContext == null) return null;

        if (httpContext.Request.RouteValues.TryGetValue(scopeKey, out var routeValue) &&
            routeValue != null &&
            int.TryParse(routeValue.ToString(), out var routeId))
        {
            return routeId;
        }

        if (httpContext.Request.Query.TryGetValue(scopeKey, out var queryValue) &&
            int.TryParse(queryValue.ToString(), out var queryId))
        {
            return queryId;
        }

        return null;
    }

    private static bool IsLocationWithinScope(int locationId, int? divisionId, int? districtId, int? blockId)
    {
        if (blockId.HasValue && locationId == blockId.Value) return true;
        if (districtId.HasValue && locationId == districtId.Value) return true;
        if (divisionId.HasValue && locationId == divisionId.Value) return true;
        return false;
    }
}
