using FluentValidation;

namespace CmScheme.Masters.Application.Features.Location.Blocks.CreateBlock;

public sealed class CreateBlockCommandValidator : AbstractValidator<CreateBlockCommand>
{
    public CreateBlockCommandValidator()
    {
        RuleFor(x => x.DistrictId)
            .GreaterThan(0).WithMessage("Valid district is required.");

        RuleFor(x => x.BlockName)
            .NotEmpty().WithMessage("Block name is required.")
            .MaximumLength(100).WithMessage("Block name must not exceed 100 characters.");

        RuleFor(x => x.BlockCode)
            .MaximumLength(10).WithMessage("Block code must not exceed 10 characters.")
            .When(x => !string.IsNullOrEmpty(x.BlockCode));
    }
}
