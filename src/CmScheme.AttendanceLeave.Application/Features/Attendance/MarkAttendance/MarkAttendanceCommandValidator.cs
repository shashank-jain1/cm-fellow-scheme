using FluentValidation;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.MarkAttendance;

public sealed class MarkAttendanceCommandValidator : AbstractValidator<MarkAttendanceCommand>
{
    public MarkAttendanceCommandValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("ApplicantId must be greater than 0.");
        RuleFor(x => x.AttendanceDate)
            .NotEmpty().WithMessage("AttendanceDate is required.");
        RuleFor(x => x.CaptureFacePath)
            .NotEmpty().WithMessage("CaptureFacePath is required.")
            .MaximumLength(500).WithMessage("CaptureFacePath must not exceed 500 characters.");
        RuleFor(x => x.FaceMatchPercentage)
            .NotNull().WithMessage("FaceMatchPercentage is required.")
            .GreaterThanOrEqualTo(80m).WithMessage("Face match must be at least 80% for attendance verification.");
        RuleFor(x => x.FaceVerificationStatus)
            .NotEmpty().WithMessage("FaceVerificationStatus is required.")
            .MaximumLength(50).WithMessage("FaceVerificationStatus must not exceed 50 characters.");
        RuleFor(x => x.AttendanceStatus)
            .NotEmpty().WithMessage("AttendanceStatus is required.")
            .MaximumLength(50).WithMessage("AttendanceStatus must not exceed 50 characters.");
    }
}
