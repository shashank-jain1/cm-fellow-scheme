using FluentValidation;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.CreateWorkAllocation;

public sealed class CreateWorkAllocationCommandValidator : AbstractValidator<CreateWorkAllocationCommand>
{
    public CreateWorkAllocationCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("ProjectId must be greater than 0.");
        RuleFor(x => x.WorkProjectId)
            .NotEmpty().WithMessage("WorkProjectId is required.")
            .MaximumLength(50).WithMessage("WorkProjectId must not exceed 50 characters.");
        RuleFor(x => x.WorkDescription)
            .NotEmpty().WithMessage("WorkDescription is required.")
            .MaximumLength(2000).WithMessage("WorkDescription must not exceed 2000 characters.");
        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required.")
            .MaximumLength(20).WithMessage("Priority must not exceed 20 characters.");
        RuleFor(x => x.SurveysPerIntern)
            .GreaterThan(0).WithMessage("SurveysPerIntern must be greater than 0.");
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .MaximumLength(50).WithMessage("Status must not exceed 50 characters.");
        RuleFor(x => x.CreatedBy)
            .NotEmpty().WithMessage("CreatedBy is required.")
            .MaximumLength(200).WithMessage("CreatedBy must not exceed 200 characters.");
    }
}
