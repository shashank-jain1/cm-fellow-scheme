using CmScheme.Registration.Application.Features.Registration.VerifyMobileOtp;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class VerifyMobileOtpCommandValidatorTests
{
    private readonly VerifyMobileOtpCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_MobileNumber_Is_Empty()
    {
        var command = new VerifyMobileOtpCommand
        {
            MobileNumber = "",
            OtpCode = "123456"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MobileNumber)
            .WithErrorMessage("Mobile number is required.");
    }

    [Fact]
    public void Should_Have_Error_When_MobileNumber_Is_Not_10_Digits()
    {
        var command = new VerifyMobileOtpCommand
        {
            MobileNumber = "12345",
            OtpCode = "123456"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MobileNumber)
            .WithErrorMessage("Mobile number must be 10 digits.");
    }

    [Fact]
    public void Should_Have_Error_When_OtpCode_Is_Invalid()
    {
        var command = new VerifyMobileOtpCommand
        {
            MobileNumber = "9876543210",
            OtpCode = "12ab"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.OtpCode)
            .WithErrorMessage("OTP code must be 6 digits.");
    }

    [Fact]
    public void Should_Be_Valid_When_All_Fields_Provided()
    {
        var command = new VerifyMobileOtpCommand
        {
            MobileNumber = "9876543210",
            OtpCode = "123456"
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}