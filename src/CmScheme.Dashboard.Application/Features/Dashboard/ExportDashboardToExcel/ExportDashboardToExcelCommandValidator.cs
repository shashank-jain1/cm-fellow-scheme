using FluentValidation;

namespace CmScheme.Dashboard.Application.Features.Dashboard.ExportDashboardToExcel;

public sealed class ExportDashboardToExcelCommandValidator : AbstractValidator<ExportDashboardToExcelCommand>
{
    public ExportDashboardToExcelCommandValidator()
    {
    }
}
