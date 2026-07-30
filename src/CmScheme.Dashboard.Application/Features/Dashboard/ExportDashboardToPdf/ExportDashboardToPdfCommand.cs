using Ardalis.Result;
using Mediator;

namespace CmScheme.Dashboard.Application.Features.Dashboard.ExportDashboardToPdf;

public sealed record ExportDashboardToPdfCommand : ICommand<Result<byte[]>>
{
    public string Role { get; init; } = null!;
}
