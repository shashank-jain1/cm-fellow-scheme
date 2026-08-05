using FluentValidation;

namespace CmScheme.Masters.Application.Features.Location.Districts.UpdateDistrict;

public sealed class UpdateDistrictCommandValidator : AbstractValidator<UpdateDistrictCommand>
{
    public UpdateDistrictCommandValidator()
    {
        RuleFor(x => x.DistrictId)
            .GreaterThan(0).WithMessage("Valid district is required.");

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
