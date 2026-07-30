namespace CmScheme.Registration.Application.Features.UserModuleAccess.Dtos;

public sealed record ModuleAccessDto
{
    public int UserModuleAccessId { get; init; }
    public int UserAccountId { get; init; }
    public string Username { get; init; } = null!;
    public string FullName { get; init; } = null!;
    public int ModuleMasterId { get; init; }
    public string ModuleCode { get; init; } = null!;
    public string ModuleName { get; init; } = null!;
    public bool CanRead { get; init; }
    public bool CanWrite { get; init; }
    public bool CanApprove { get; init; }
    public bool CanExport { get; init; }
    public int? DivisionId { get; init; }
    public int? DistrictId { get; init; }
    public int? BlockId { get; init; }
    public bool IsActive { get; init; }
}

public sealed record UserAccessSummaryDto
{
    public int UserAccountId { get; init; }
    public string Username { get; init; } = null!;
    public string FullName { get; init; } = null!;
    public string Role { get; init; } = null!;
    public int? DivisionId { get; init; }
    public string? DivisionName { get; init; }
    public List<ModuleAccessDto> ModuleAccesses { get; init; } = [];
}

public sealed record ModuleAccessItemDto
{
    public int ModuleMasterId { get; init; }
    public string ModuleCode { get; init; } = null!;
    public string ModuleName { get; init; } = null!;
    public bool CanRead { get; init; }
    public bool CanWrite { get; init; }
    public bool CanApprove { get; init; }
    public bool CanExport { get; init; }
    public int? DivisionId { get; init; }
    public int? DistrictId { get; init; }
    public int? BlockId { get; init; }
}

public sealed record ModuleMasterDto
{
    public int ModuleMasterId { get; init; }
    public string ModuleCode { get; init; } = null!;
    public string ModuleName { get; init; } = null!;
    public string? Description { get; init; }
    public int SortOrder { get; init; }
    public bool IsActive { get; init; }
}
