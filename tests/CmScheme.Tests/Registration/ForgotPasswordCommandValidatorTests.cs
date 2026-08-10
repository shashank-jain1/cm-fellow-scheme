using CmScheme.Registration.Application.Features.UserAccount.ForgotPassword;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class ForgotPasswordCommandValidatorTests
{
    private readonly ForgotPasswordCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var command = new ForgotPasswordCommand
        {
            Email = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Email is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var command = new ForgotPasswordCommand
        {
            Email = "not-an-email"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Invalid email address.");
    }

    [Fact]
    public void Should_Be_Valid_When_Email_Is_Provided()
    {
        var command = new ForgotPasswordCommand
        {
            Email = "fellow@test.gov.in"
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}