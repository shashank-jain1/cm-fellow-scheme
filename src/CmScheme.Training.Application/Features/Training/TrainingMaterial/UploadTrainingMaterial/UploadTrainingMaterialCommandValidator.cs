using FluentValidation;

namespace CmScheme.Training.Application.Features.Training.TrainingMaterial.UploadTrainingMaterial;

public sealed class UploadTrainingMaterialCommandValidator
    : AbstractValidator<UploadTrainingMaterialCommand>
{
    public UploadTrainingMaterialCommandValidator()
    {
        RuleFor(x => x.TrainingScheduleId)
            .GreaterThan(0).WithMessage("TrainingScheduleId is required.");

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage("MaterialName is required.")
            .MaximumLength(200).WithMessage("MaterialName must not exceed 200 characters.");

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("FileName is required.");

        RuleFor(x => x.FileSize)
            .GreaterThan(0).WithMessage("FileSize must be greater than 0.");
    }
}
