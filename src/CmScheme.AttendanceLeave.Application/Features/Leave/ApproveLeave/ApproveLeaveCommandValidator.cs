using FluentValidation;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.ApproveLeave;

public sealed class ApproveLeaveCommandValidator : AbstractValidator<ApproveLeaveCommand>
{
    public ApproveLeaveCommandValidator()
    {
        RuleFor(x => x.LeaveApplicationId)
            .GreaterThan(0).WithMessage("LeaveApplicationId must be greater than 0.");
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .MaximumLength(50).WithMessage("Status must not exceed 50 characters.");
        RuleFor(x => x.Remarks)
            .NotEmpty().WithMessage("Remarks is required.");
    }
}
