using FluentValidation;

namespace CmScheme.Masters.Application.Features.Works.UpdateWork;

public sealed class UpdateWorkCommandValidator : AbstractValidator<UpdateWorkCommand>
{
    public UpdateWorkCommandValidator()
    {
        RuleFor(x => x.WorkId)
            .GreaterThan(0).WithMessage("Work ID is required.");
        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("Project ID is required.");
        RuleFor(x => x.WorkName)
            .NotEmpty().WithMessage("Work Name is required.")
            .MaximumLength(250).WithMessage("Work Name must not exceed 250 characters.");
        RuleFor(x => x.WorkDescription)
            .MaximumLength(1000).WithMessage("Work Description must not exceed 1000 characters.");
        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required.")
            .MaximumLength(20).WithMessage("Priority must not exceed 20 characters.");
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start Date is required.");
        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End Date is required.")
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End Date must be on or after Start Date.");
        RuleFor(x => x.AssignedTo)
            .NotEmpty().WithMessage("Assigned To is required.")
            .MaximumLength(150).WithMessage("Assigned To must not exceed 150 characters.");
        RuleFor(x => x.Remarks)
            .MaximumLength(500).WithMessage("Remarks must not exceed 500 characters.");
    }
}
