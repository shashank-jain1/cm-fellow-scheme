using FluentValidation;

namespace CmScheme.HelpDesk.Application.Features.Sla.CheckSlaBreaches;

public sealed class CheckSlaBreachesCommandValidator : AbstractValidator<CheckSlaBreachesCommand>
{
    public CheckSlaBreachesCommandValidator()
    {
    }
}
