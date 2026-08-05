using FluentValidation;

namespace CmScheme.Masters.Application.Features.Location.States.DeleteState;

public sealed class DeleteStateCommandValidator : AbstractValidator<DeleteStateCommand>
{
    public DeleteStateCommandValidator()
    {
        RuleFor(x => x.StateId)
            .GreaterThan(0).WithMessage("StateId must be greater than 0.");
    }
}
