using FluentValidation;

namespace CmScheme.Registration.Application.Features.Registration.SendOtp;

public sealed class SendOtpCommandValidator : AbstractValidator<SendOtpCommand>
{
    public SendOtpCommandValidator()
    {
        RuleFor(x => x.MobileNumber)
            .NotEmpty().WithMessage("Mobile number is required.")
            .Matches(@"^[0-9]{10}$").WithMessage("Mobile number must be 10 digits.");
    }
}
