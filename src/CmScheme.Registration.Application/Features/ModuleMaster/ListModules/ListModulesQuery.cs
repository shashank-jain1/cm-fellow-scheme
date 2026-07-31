using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.ModuleMaster.ListModules;

public sealed record ListModulesQuery : IQuery<Result<List<ListModulesResult>>>;

public sealed record ListModulesResult
{
    public int ModuleMasterId { get; init; }
    public string ModuleCode { get; init; } = null!;
    public string ModuleName { get; init; } = null!;
    public string? Description { get; init; }
    public int SortOrder { get; init; }
    public int? ParentModuleMasterId { get; init; }
    public List<ListModulesResult> Children { get; init; } = new();
}
