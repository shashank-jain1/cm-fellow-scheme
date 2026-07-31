namespace CmScheme.Registration.Core.Dtos;

public sealed record ModuleMasterDto
{
    public int ModuleMasterId { get; init; }
    public string ModuleCode { get; init; } = null!;
    public string ModuleName { get; init; } = null!;
    public string? Description { get; init; }
    public int SortOrder { get; init; }
    public bool IsActive { get; init; }
}
