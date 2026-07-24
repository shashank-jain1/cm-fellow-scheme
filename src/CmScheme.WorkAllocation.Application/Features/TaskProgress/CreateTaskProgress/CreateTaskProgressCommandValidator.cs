using FluentValidation;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.CreateTaskProgress;

public sealed class CreateTaskProgressCommandValidator : AbstractValidator<CreateTaskProgressCommand>
{
    public CreateTaskProgressCommandValidator()
    {
        RuleFor(x => x.WorkAllocationId)
            .GreaterThan(0).WithMessage("WorkAllocationId must be greater than 0.");
        RuleFor(x => x.ProjectName)
            .NotEmpty().WithMessage("ProjectName is required.")
            .MaximumLength(200).WithMessage("ProjectName must not exceed 200 characters.");
        RuleFor(x => x.WorkProject)
            .NotEmpty().WithMessage("WorkProject is required.")
            .MaximumLength(200).WithMessage("WorkProject must not exceed 200 characters.");
        RuleFor(x => x.WorkDescription)
            .NotEmpty().WithMessage("WorkDescription is required.")
            .MaximumLength(2000).WithMessage("WorkDescription must not exceed 2000 characters.");
        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required.")
            .MaximumLength(20).WithMessage("Priority must not exceed 20 characters.");
        RuleFor(x => x.NumberOfSurveys)
            .GreaterThan(0).WithMessage("NumberOfSurveys must be greater than 0.");
        RuleFor(x => x.CompletedSurveys)
            .GreaterThanOrEqualTo(0).WithMessage("CompletedSurveys must be greater than or equal to 0.");
        RuleFor(x => x.WorkStatus)
            .NotEmpty().WithMessage("WorkStatus is required.")
            .MaximumLength(50).WithMessage("WorkStatus must not exceed 50 characters.");
        RuleFor(x => x.CompletionPercentage)
            .InclusiveBetween(0, 100).WithMessage("CompletionPercentage must be between 0 and 100.");
    }
}
