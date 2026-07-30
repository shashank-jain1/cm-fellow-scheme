using FluentValidation;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.UpdateTaskProgress;

public sealed class UpdateTaskProgressCommandValidator : AbstractValidator<UpdateTaskProgressCommand>
{
    public UpdateTaskProgressCommandValidator()
    {
        RuleFor(x => x.WorkAllocationId)
            .GreaterThan(0).WithMessage("WorkAllocationId must be greater than 0.");
        RuleFor(x => x.ProgressNotes)
            .NotEmpty().WithMessage("ProgressNotes is required.")
            .MaximumLength(500).WithMessage("ProgressNotes must not exceed 500 characters.");
        RuleFor(x => x.ProgressPercentage)
            .InclusiveBetween(0, 100).WithMessage("ProgressPercentage must be between 0 and 100.");
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(s => s == "InProgress" || s == "Completed" || s == "Blocked")
            .WithMessage("Status must be InProgress, Completed, or Blocked.");
    }
}
