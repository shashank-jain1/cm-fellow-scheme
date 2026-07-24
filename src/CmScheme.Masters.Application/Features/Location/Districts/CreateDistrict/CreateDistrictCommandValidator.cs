using FluentValidation;

namespace CmScheme.Masters.Application.Features.Location.Districts.CreateDistrict;

public sealed class CreateDistrictCommandValidator : AbstractValidator<CreateDistrictCommand>
{
    public CreateDistrictCommandValidator()
    {
        RuleFor(x => x.DivisionId)
            .GreaterThan(0).WithMessage("Valid division is required.");

        RuleFor(x => x.DistrictName)
            .NotEmpty().WithMessage("District name is required.")
            .MaximumLength(100).WithMessage("District name must not exceed 100 characters.");

        RuleFor(x => x.DistrictCode)
            .MaximumLength(10).WithMessage("District code must not exceed 10 characters.")
            .When(x => !string.IsNullOrEmpty(x.DistrictCode));
    }
}
