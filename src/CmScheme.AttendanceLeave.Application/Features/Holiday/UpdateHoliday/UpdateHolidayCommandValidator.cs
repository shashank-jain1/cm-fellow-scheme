using FluentValidation;

namespace CmScheme.AttendanceLeave.Application.Features.Holiday.UpdateHoliday;

public sealed class UpdateHolidayCommandValidator : AbstractValidator<UpdateHolidayCommand>
{
    public UpdateHolidayCommandValidator()
    {
        RuleFor(x => x.HolidayId)
            .GreaterThan(0).WithMessage("Holiday ID is required.");
        RuleFor(x => x.HolidayName)
            .NotEmpty().WithMessage("Holiday name is required.")
            .MaximumLength(200).WithMessage("Holiday name must not exceed 200 characters.");
        RuleFor(x => x.HolidayDate)
            .NotEmpty().WithMessage("Holiday date is required.");
    }
}
