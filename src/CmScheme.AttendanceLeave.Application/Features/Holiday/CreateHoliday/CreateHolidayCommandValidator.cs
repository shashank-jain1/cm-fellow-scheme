using FluentValidation;

namespace CmScheme.AttendanceLeave.Application.Features.Holiday.CreateHoliday;

public sealed class CreateHolidayCommandValidator : AbstractValidator<CreateHolidayCommand>
{
    public CreateHolidayCommandValidator()
    {
        RuleFor(x => x.HolidayName)
            .NotEmpty().WithMessage("Holiday name is required.")
            .MaximumLength(200).WithMessage("Holiday name must not exceed 200 characters.");
        RuleFor(x => x.HolidayDate)
            .NotEmpty().WithMessage("Holiday date is required.");
    }
}
