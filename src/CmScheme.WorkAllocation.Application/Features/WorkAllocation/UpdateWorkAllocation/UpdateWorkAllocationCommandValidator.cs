using FluentValidation;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.UpdateWorkAllocation;

public sealed class UpdateWorkAllocationCommandValidator : AbstractValidator<UpdateWorkAllocationCommand>
{
    public UpdateWorkAllocationCommandValidator()
    {
        RuleFor(x => x.WorkAllocationId)
            .GreaterThan(0).WithMessage("WorkAllocationId must be greater than 0.");
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
        RuleFor(x => x.DurationDays)
            .GreaterThan(0).WithMessage("DurationDays must be greater than 0.");
        RuleFor(x => x.SurveysPerIntern)
            .GreaterThan(0).WithMessage("SurveysPerIntern must be greater than 0.");
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .MaximumLength(50).WithMessage("Status must not exceed 50 characters.");
        RuleFor(x => x.ModifiedBy)
            .NotEmpty().WithMessage("ModifiedBy is required.")
            .MaximumLength(200).WithMessage("ModifiedBy must not exceed 200 characters.");
    }
}
