using FluentValidation;

namespace CmScheme.Masters.Application.Features.Location.Divisions.DeleteDivision;

public sealed class DeleteDivisionCommandValidator : AbstractValidator<DeleteDivisionCommand>
{
    public DeleteDivisionCommandValidator()
    {
        RuleFor(x => x.DivisionId)
            .GreaterThan(0).WithMessage("Valid division is required.");
    }
}
