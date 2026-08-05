using FluentValidation;

namespace CmScheme.Masters.Application.Features.Location.Blocks.DeleteBlock;

public sealed class DeleteBlockCommandValidator : AbstractValidator<DeleteBlockCommand>
{
    public DeleteBlockCommandValidator()
    {
        RuleFor(x => x.BlockId)
            .GreaterThan(0).WithMessage("Valid block is required.");
    }
}
