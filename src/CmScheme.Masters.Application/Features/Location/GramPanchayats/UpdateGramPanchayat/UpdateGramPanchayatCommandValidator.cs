using FluentValidation;

namespace CmScheme.Masters.Application.Features.Location.GramPanchayats.UpdateGramPanchayat;

public sealed class UpdateGramPanchayatCommandValidator : AbstractValidator<UpdateGramPanchayatCommand>
{
    public UpdateGramPanchayatCommandValidator()
    {
        RuleFor(x => x.GramPanchayatId)
            .GreaterThan(0).WithMessage("Valid gram panchayat is required.");

        RuleFor(x => x.BlockId)
            .GreaterThan(0).WithMessage("Valid block is required.");

        RuleFor(x => x.GramPanchayatName)
            .NotEmpty().WithMessage("Gram Panchayat name is required.")
            .MaximumLength(150).WithMessage("Gram Panchayat name must not exceed 150 characters.");

        RuleFor(x => x.GPCode)
            .MaximumLength(20).WithMessage("GP code must not exceed 20 characters.")
            .When(x => !string.IsNullOrEmpty(x.GPCode));
    }
}
