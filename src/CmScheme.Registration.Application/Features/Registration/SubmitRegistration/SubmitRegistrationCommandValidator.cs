using FluentValidation;

namespace CmScheme.Registration.Application.Features.Registration.SubmitRegistration;

public sealed class SubmitRegistrationCommandValidator : AbstractValidator<SubmitRegistrationCommand>
{
    public SubmitRegistrationCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

        RuleFor(x => x.FatherName)
            .NotEmpty().WithMessage("Father name is required.")
            .MaximumLength(150).WithMessage("Father name must not exceed 150 characters.");

        RuleFor(x => x.MobileNumber)
            .NotEmpty().WithMessage("Mobile number is required.")
            .Matches(@"^\d{10}$").WithMessage("Mobile number must be exactly 10 digits.");

        RuleFor(x => x.EmailId)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.")
            .MaximumLength(150).WithMessage("Email must not exceed 150 characters.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.");

        RuleFor(x => x.PermanentAddress)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(500).WithMessage("Address must not exceed 500 characters.");

        RuleFor(x => x.PinCode)
            .NotEmpty().WithMessage("Pin code is required.")
            .Matches(@"^\d{6}$").WithMessage("Pin code must be exactly 6 digits.");

        RuleFor(x => x.BoardUniversityName)
            .NotEmpty().WithMessage("Board/University name is required.")
            .MaximumLength(200).WithMessage("Board/University name must not exceed 200 characters.");

        RuleFor(x => x.PassingYear)
            .InclusiveBetween(1950, DateTime.UtcNow.Year + 1).WithMessage("Invalid passing year.");

        RuleFor(x => x.PercentageCGPA)
            .InclusiveBetween(0, 100).WithMessage("Percentage/CGPA must be between 0 and 100.");

        RuleFor(x => x.AadhaarNumber)
            .Matches(@"^\d{12}$").When(x => !string.IsNullOrEmpty(x.AadhaarNumber))
            .WithMessage("Aadhaar number must be exactly 12 digits.");

        RuleFor(x => x.PanNumber)
            .Matches(@"^[A-Z]{5}\d{4}[A-Z]$").When(x => !string.IsNullOrEmpty(x.PanNumber))
            .WithMessage("Invalid PAN number format.");

        RuleFor(x => x.DeclarationAccepted)
            .Equal(true).WithMessage("Declaration must be accepted.");
    }
}
