using FluentValidation;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.CheckOutAttendance;

public sealed class CheckOutAttendanceCommandValidator : AbstractValidator<CheckOutAttendanceCommand>
{
    public CheckOutAttendanceCommandValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("ApplicantId is required.");
        RuleFor(x => x.AttendanceDate)
            .NotEmpty().WithMessage("AttendanceDate is required.");
    }
}
