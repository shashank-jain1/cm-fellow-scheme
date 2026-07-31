namespace CmScheme.Registration.Core.Dtos;

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
