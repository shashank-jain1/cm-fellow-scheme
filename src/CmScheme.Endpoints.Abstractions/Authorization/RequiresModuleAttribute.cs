using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CmScheme.Endpoints.Abstractions.Authorization;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public sealed class RequiresModuleAttribute : Attribute, IAuthorizeData
{
    public string ModuleCode { get; }

    public string Permission { get; }

    public bool RequireScope { get; }

    public string ScopeKey { get; }

    public string? Policy { get; set; }

    public string? Roles { get; set; }

    public string? AuthenticationSchemes { get; set; }

    public RequiresModuleAttribute(string moduleCode, string permission, bool requireScope = true, string scopeKey = "id")
    {
        ModuleCode = moduleCode;
        Permission = permission;
        RequireScope = requireScope;
        ScopeKey = scopeKey;
    }

    public void Apply(AuthorizationFilterContext context)
    {
        context.HttpContext.Items["RequiredModule"] = ModuleCode;
        context.HttpContext.Items["RequiredPermission"] = Permission;
        context.HttpContext.Items["RequireScope"] = RequireScope;
        context.HttpContext.Items["ScopeKey"] = ScopeKey;
    }
}
