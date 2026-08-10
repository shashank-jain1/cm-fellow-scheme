using FluentValidation;

namespace CmScheme.Registration.Application.Features.Registration.VerifyMobileOtp;

public sealed class VerifyMobileOtpCommandValidator : AbstractValidator<VerifyMobileOtpCommand>
{
    public VerifyMobileOtpCommandValidator()
    {
        RuleFor(x => x.MobileNumber)
            .NotEmpty().WithMessage("Mobile number is required.")
            .Matches(@"^[0-9]{10}$").WithMessage("Mobile number must be 10 digits.");

        RuleFor(x => x.OtpCode)
            .NotEmpty().WithMessage("OTP code is required.")
            .Length(6).WithMessage("OTP code must be 6 digits.")
            .Matches("^[0-9]+$").WithMessage("OTP code must contain only digits.");
    }
}
