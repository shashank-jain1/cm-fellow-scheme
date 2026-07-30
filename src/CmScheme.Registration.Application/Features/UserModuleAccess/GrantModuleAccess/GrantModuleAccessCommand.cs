using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.GrantModuleAccess;

public sealed record GrantModuleAccessCommand : ICommand<Result<int>>
{
    public int UserAccountId { get; init; }
    public int ModuleMasterId { get; init; }
    public bool CanRead { get; init; } = true;
    public bool CanWrite { get; init; }
    public bool CanApprove { get; init; }
    public bool CanExport { get; init; }
    public int? DivisionId { get; init; }
    public int? DistrictId { get; init; }
    public int? BlockId { get; init; }
    public int PerformedBy { get; init; }
}
