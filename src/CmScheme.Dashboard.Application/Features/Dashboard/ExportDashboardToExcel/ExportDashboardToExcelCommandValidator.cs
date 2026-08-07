using FluentValidation;

namespace CmScheme.Dashboard.Application.Features.Dashboard.ExportDashboardToExcel;

public sealed class ExportDashboardToExcelCommandValidator : AbstractValidator<ExportDashboardToExcelCommand>
{
    public ExportDashboardToExcelCommandValidator()
    {
        When(x => x.StartDate.HasValue && x.EndDate.HasValue, () =>
        {
            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be after start date.");
        });
    }
}
