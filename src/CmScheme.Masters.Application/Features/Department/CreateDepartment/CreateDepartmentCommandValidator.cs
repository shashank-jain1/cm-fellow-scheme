using FluentValidation;

namespace CmScheme.Masters.Application.Features.Department.CreateDepartment;

public sealed class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.DepartmentName)
            .NotEmpty().WithMessage("Department name is required.")
            .MaximumLength(150).WithMessage("Department name must not exceed 150 characters.");

        RuleFor(x => x.DepartmentCode)
            .MaximumLength(50).WithMessage("Department code must not exceed 50 characters.");
    }
}
