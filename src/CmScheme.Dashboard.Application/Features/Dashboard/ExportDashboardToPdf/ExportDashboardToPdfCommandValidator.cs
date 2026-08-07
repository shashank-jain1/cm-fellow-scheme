using FluentValidation;

namespace CmScheme.Dashboard.Application.Features.Dashboard.ExportDashboardToPdf;

public sealed class ExportDashboardToPdfCommandValidator : AbstractValidator<ExportDashboardToPdfCommand>
{
    public ExportDashboardToPdfCommandValidator()
    {
        When(x => x.StartDate.HasValue && x.EndDate.HasValue, () =>
        {
            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be after start date.");
        });
    }
}
