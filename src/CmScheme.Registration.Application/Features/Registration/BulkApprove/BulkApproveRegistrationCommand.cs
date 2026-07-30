using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.BulkApprove;

public sealed record BulkApproveRegistrationCommand : ICommand<Result<int>>
{
    public IReadOnlyList<int> ApplicantIds { get; init; } = [];
    public string Action { get; init; } = null!;
    public string? Remarks { get; init; }
    public int PerformedBy { get; init; }
}
