using FluentValidation;

namespace CmScheme.Masters.Application.Features.Projects.CreateProject;

public sealed class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.ProjectName)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(250).WithMessage("Project name must not exceed 250 characters.");

        RuleFor(x => x.ProjectCode)
            .NotEmpty().WithMessage("Project code is required.")
            .MaximumLength(20).WithMessage("Project code must not exceed 20 characters.");

        RuleFor(x => x.ProjectDescription)
            .MaximumLength(1000).WithMessage("Project description must not exceed 1000 characters.")
            .When(x => !string.IsNullOrEmpty(x.ProjectDescription));

        RuleFor(x => x.DepartmentName)
            .NotEmpty().WithMessage("Department name is required.")
            .MaximumLength(150).WithMessage("Department name must not exceed 150 characters.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be after start date.");

        RuleFor(x => x.ProjectIncharge)
            .NotEmpty().WithMessage("Project incharge is required.")
            .MaximumLength(150).WithMessage("Project incharge must not exceed 150 characters.");

        RuleFor(x => x.BudgetAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Budget amount must be non-negative.")
            .When(x => x.BudgetAmount.HasValue);

        RuleFor(x => x.ProjectDocumentPath)
            .MaximumLength(255).WithMessage("Document path must not exceed 255 characters.")
            .When(x => !string.IsNullOrEmpty(x.ProjectDocumentPath));
    }
}
