using FluentValidation;

namespace CmScheme.Performance.Application.Features.Performance.RecordEvaluationRemarks;

public sealed class RecordEvaluationRemarksCommandValidator : AbstractValidator<RecordEvaluationRemarksCommand>
{
    public RecordEvaluationRemarksCommandValidator()
    {
        RuleFor(x => x.PerformanceEvaluationId)
            .GreaterThan(0).WithMessage("PerformanceEvaluationId must be greater than 0.");
        RuleFor(x => x.EvaluationRemarks)
            .NotEmpty().WithMessage("EvaluationRemarks is required.")
            .MaximumLength(2000).WithMessage("EvaluationRemarks must not exceed 2000 characters.");
        RuleFor(x => x.EvaluatedBy)
            .NotEmpty().WithMessage("EvaluatedBy is required.")
            .MaximumLength(200).WithMessage("EvaluatedBy must not exceed 200 characters.");
    }
}
