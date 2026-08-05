using FluentValidation;

namespace CmScheme.Dashboard.Application.Features.Dashboard.ExportDashboardToPdf;

public sealed class ExportDashboardToPdfCommandValidator : AbstractValidator<ExportDashboardToPdfCommand>
{
    public ExportDashboardToPdfCommandValidator()
    {
    }
}
