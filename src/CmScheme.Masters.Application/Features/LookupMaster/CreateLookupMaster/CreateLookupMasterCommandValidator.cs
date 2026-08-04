using FluentValidation;

namespace CmScheme.Masters.Application.Features.LookupMaster.CreateLookupMaster;

public sealed class CreateLookupMasterCommandValidator : AbstractValidator<CreateLookupMasterCommand>
{
    public CreateLookupMasterCommandValidator()
    {
        RuleFor(x => x.MasterType)
            .NotEmpty().WithMessage("MasterType is required.")
            .MaximumLength(50).WithMessage("MasterType must not exceed 50 characters.");

        RuleFor(x => x.Label)
            .NotEmpty().WithMessage("Label is required.")
            .MaximumLength(100).WithMessage("Label must not exceed 100 characters.");

        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Value is required.")
            .MaximumLength(100).WithMessage("Value must not exceed 100 characters.");
    }
}
