using Ardalis.Result;
using Mediator;

namespace CmScheme.Dashboard.Application.Features.Dashboard.ExportDashboardToExcel;

public sealed record ExportDashboardToExcelCommand : ICommand<Result<byte[]>>
{
    public string Role { get; init; } = null!;
}
