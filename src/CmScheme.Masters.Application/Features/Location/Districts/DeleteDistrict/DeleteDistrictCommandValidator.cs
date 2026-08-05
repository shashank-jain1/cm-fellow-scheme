using FluentValidation;

namespace CmScheme.Masters.Application.Features.Location.Districts.DeleteDistrict;

public sealed class DeleteDistrictCommandValidator : AbstractValidator<DeleteDistrictCommand>
{
    public DeleteDistrictCommandValidator()
    {
        RuleFor(x => x.DistrictId)
            .GreaterThan(0).WithMessage("Valid district is required.");
    }
}
