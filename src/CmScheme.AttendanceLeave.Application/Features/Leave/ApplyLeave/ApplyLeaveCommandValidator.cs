using FluentValidation;
using Microsoft.EntityFrameworkCore;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.Common.Core;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.ApplyLeave;

public sealed class ApplyLeaveCommandValidator : AbstractValidator<ApplyLeaveCommand>
{
    public ApplyLeaveCommandValidator(IAttendanceLeaveCommandDbContext dbContext)
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

        // "Same date leave not apply" — an overlapping request blocks a new one, but a
        // rejected or cancelled request must not block those dates forever.
        RuleFor(x => x)
            .MustAsync(async (command, cancellationToken) =>
            {
                bool hasExistingLeave = await dbContext.LeaveApplications
                    .AnyAsync(la =>
                        la.ApplicantId == command.ApplicantId &&
                        la.Status != Statuses.Leave.Rejected &&
                        la.Status != Statuses.Leave.Cancelled &&
                        la.FromDate <= command.ToDate &&
                        la.ToDate >= command.FromDate,
                        cancellationToken);
                return !hasExistingLeave;
            })
            .WithMessage("Leave application already exists for the selected date range. Same-date leave cannot be applied twice.");
    }
}
