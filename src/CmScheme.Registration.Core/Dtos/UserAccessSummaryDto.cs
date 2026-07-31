namespace CmScheme.Registration.Core.Dtos;

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
