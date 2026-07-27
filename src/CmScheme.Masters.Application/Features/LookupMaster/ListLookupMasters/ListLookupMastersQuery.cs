using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.LookupMaster.ListLookupMasters;

public sealed record ListLookupMastersQuery : IQuery<Result<IReadOnlyList<LookupMasterListItem>>>
{
    public string? MasterType { get; init; }
}

public sealed record LookupMasterListItem
{
    public int LookupMasterId { get; init; }
    public string MasterType { get; init; } = null!;
    public string Label { get; init; } = null!;
    public string Value { get; init; } = null!;
    public int SortOrder { get; init; }
}
