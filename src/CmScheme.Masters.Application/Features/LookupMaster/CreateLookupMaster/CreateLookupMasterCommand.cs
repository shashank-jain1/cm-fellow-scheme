using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.LookupMaster.CreateLookupMaster;

public sealed record CreateLookupMasterCommand : ICommand<Result<int>>
{
    public string MasterType { get; init; } = null!;
    public string Label { get; init; } = null!;
    public string Value { get; init; } = null!;
    public int SortOrder { get; init; }
}
