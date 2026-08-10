using CmScheme.Registration.Application.Features.UserAccount.ResetPassword;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class ResetPasswordCommandValidatorTests
{
    private readonly ResetPasswordCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Token_Is_Empty()
    {
        var command = new ResetPasswordCommand
        {
            Token = "",
            NewPassword = "Strong123"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Token)
            .WithErrorMessage("Reset token is required.");
    }

    [Fact]
    public void Should_Have_Error_When_NewPassword_Is_Too_Short()
    {
        var command = new ResetPasswordCommand
        {
            Token = "reset-token",
            NewPassword = "Short1"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.NewPassword)
            .WithErrorMessage("Password must be at least 8 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_NewPassword_Missing_Uppercase()
    {
        var command = new ResetPasswordCommand
        {
            Token = "reset-token",
            NewPassword = "strong123"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.NewPassword)
            .WithErrorMessage("Password must contain at least one uppercase letter.");
    }

    [Fact]
    public void Should_Have_Error_When_NewPassword_Missing_Digit()
    {
        var command = new ResetPasswordCommand
        {
            Token = "reset-token",
            NewPassword = "StrongPass"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.NewPassword)
            .WithErrorMessage("Password must contain at least one digit.");
    }

    [Fact]
    public void Should_Be_Valid_When_All_Fields_Provided()
    {
        var command = new ResetPasswordCommand
        {
            Token = "reset-token",
            NewPassword = "Strong123"
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}
