using Ardalis.Result;
using Mediator;

namespace CmScheme.Dashboard.Application.Features.Dashboard.ExportDashboardToExcel;

public sealed record ExportDashboardToExcelCommand : ICommand<Result<byte[]>>
{
    public string Role { get; init; } = "Admin";
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int? ProjectId { get; init; }
    public int? DivisionId { get; init; }
}
