using FluentValidation;

namespace CmScheme.Masters.Application.Features.Works.CreateWork;

public sealed class CreateWorkCommandValidator : AbstractValidator<CreateWorkCommand>
{
    public CreateWorkCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("Valid project is required.");

        RuleFor(x => x.WorkName)
            .NotEmpty().WithMessage("Work name is required.")
            .MaximumLength(250).WithMessage("Work name must not exceed 250 characters.");

        RuleFor(x => x.WorkDescription)
            .MaximumLength(1000).WithMessage("Work description must not exceed 1000 characters.")
            .When(x => !string.IsNullOrEmpty(x.WorkDescription));

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required.")
            .MaximumLength(20).WithMessage("Priority must not exceed 20 characters.")
            .Must(p => new[] { "Low", "Medium", "High", "Critical" }.Contains(p))
            .WithMessage("Priority must be Low, Medium, High, or Critical.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be after start date.");

        RuleFor(x => x.AssignedTo)
            .NotEmpty().WithMessage("Assigned to is required.")
            .MaximumLength(150).WithMessage("Assigned to must not exceed 150 characters.");

        RuleFor(x => x.Remarks)
            .MaximumLength(500).WithMessage("Remarks must not exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Remarks));
    }
}
