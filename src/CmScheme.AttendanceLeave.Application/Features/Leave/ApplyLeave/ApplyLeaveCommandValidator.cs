using FluentValidation;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.ApplyLeave;

public sealed class ApplyLeaveCommandValidator : AbstractValidator<ApplyLeaveCommand>
{
    public ApplyLeaveCommandValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("ApplicantId must be greater than 0.");
        RuleFor(x => x.LeaveType)
            .NotEmpty().WithMessage("LeaveType is required.")
            .MaximumLength(100).WithMessage("LeaveType must not exceed 100 characters.");
        RuleFor(x => x.FromDate)
            .NotEmpty().WithMessage("FromDate is required.");
        RuleFor(x => x.ToDate)
            .NotEmpty().WithMessage("ToDate is required.")
            .GreaterThanOrEqualTo(x => x.FromDate).WithMessage("ToDate must be on or after FromDate.");
        RuleFor(x => x.NumberOfDays)
            .GreaterThan(0).WithMessage("NumberOfDays must be greater than 0.");
        RuleFor(x => x.HalfDayFullDay)
            .NotEmpty().WithMessage("HalfDayFullDay is required.")
            .MaximumLength(20).WithMessage("HalfDayFullDay must not exceed 20 characters.");
        RuleFor(x => x.LeaveReason)
            .NotEmpty().WithMessage("LeaveReason is required.")
            .MaximumLength(1000).WithMessage("LeaveReason must not exceed 1000 characters.");
        RuleFor(x => x.ReportingManagerName)
            .NotEmpty().WithMessage("ReportingManagerName is required.")
            .MaximumLength(200).WithMessage("ReportingManagerName must not exceed 200 characters.");
        RuleFor(x => x.CreatedBy)
            .NotEmpty().WithMessage("CreatedBy is required.")
            .MaximumLength(200).WithMessage("CreatedBy must not exceed 200 characters.");
    }
}
