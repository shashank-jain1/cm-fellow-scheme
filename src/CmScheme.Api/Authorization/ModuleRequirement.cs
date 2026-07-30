using Microsoft.AspNetCore.Authorization;

namespace CmScheme.Api.Authorization;

public sealed class ModuleRequirement : IAuthorizationRequirement
{
    public string ModuleCode { get; }

    public string Permission { get; }

    public bool RequireScope { get; }

    public string ScopeKey { get; }

    public ModuleRequirement(string moduleCode, string permission, bool requireScope = true, string scopeKey = "id")
    {
        ModuleCode = moduleCode;
        Permission = permission;
        RequireScope = requireScope;
        ScopeKey = scopeKey;
    }
}
