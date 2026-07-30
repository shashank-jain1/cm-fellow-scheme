using FluentValidation;

namespace CmScheme.WorkAllocation.Application.Features.TaskAttachments.UploadTaskAttachment;

public sealed class UploadTaskAttachmentCommandValidator : AbstractValidator<UploadTaskAttachmentCommand>
{
    public UploadTaskAttachmentCommandValidator()
    {
        RuleFor(x => x.WorkAllocationId)
            .GreaterThan(0).WithMessage("WorkAllocationId must be greater than 0.");
        RuleFor(x => x.File)
            .NotNull().WithMessage("File is required.");
    }
}
