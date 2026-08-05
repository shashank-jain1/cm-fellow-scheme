using FluentValidation;

namespace CmScheme.AttendanceLeave.Application.Features.Holiday.DeleteHoliday;

public sealed class DeleteHolidayCommandValidator : AbstractValidator<DeleteHolidayCommand>
{
    public DeleteHolidayCommandValidator()
    {
        RuleFor(x => x.HolidayId)
            .GreaterThan(0).WithMessage("Holiday ID is required.");
    }
}
