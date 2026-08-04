using FluentValidation;

namespace CmScheme.Masters.Application.Features.Projects.UpdateProject;

public sealed class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("ProjectId must be greater than 0.");

        RuleFor(x => x.ProjectName)
            .NotEmpty().WithMessage("ProjectName is required.")
            .MaximumLength(250).WithMessage("ProjectName must not exceed 250 characters.");

        RuleFor(x => x.ProjectCode)
            .NotEmpty().WithMessage("ProjectCode is required.")
            .MaximumLength(20).WithMessage("ProjectCode must not exceed 20 characters.");

        RuleFor(x => x.DepartmentName)
            .NotEmpty().WithMessage("DepartmentName is required.")
            .MaximumLength(150).WithMessage("DepartmentName must not exceed 150 characters.");

        RuleFor(x => x.ProjectIncharge)
            .NotEmpty().WithMessage("ProjectIncharge is required.")
            .MaximumLength(150).WithMessage("ProjectIncharge must not exceed 150 characters.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("EndDate is required.")
            .GreaterThan(x => x.StartDate).WithMessage("EndDate must be after StartDate.");
    }
}
