using CmScheme.Registration.Application.Features.UserAccount.CreateUserAccount;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class CreateUserAccountCommandValidatorTests
{
    private readonly CreateUserAccountCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ApplicantId_Is_Zero()
    {
        var command = new CreateUserAccountCommand
        {
            ApplicantId = 0,
            Username = "fellow",
            Password = "Strong1!",
            Role = "Fellow",
            CreatedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId)
            .WithErrorMessage("Applicant ID must be greater than zero.");
    }

    [Fact]
    public void Should_Have_Error_When_Username_Is_Empty()
    {
        var command = new CreateUserAccountCommand
        {
            ApplicantId = 1,
            Username = "",
            Password = "Strong1!",
            Role = "Fellow",
            CreatedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Username)
            .WithErrorMessage("Username is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Username_Exceeds_100_Characters()
    {
        var command = new CreateUserAccountCommand
        {
            ApplicantId = 1,
            Username = new string('u', 101),
            Password = "Strong1!",
            Role = "Fellow",
            CreatedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Username)
            .WithErrorMessage("Username must not exceed 100 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Weak()
    {
        var command = new CreateUserAccountCommand
        {
            ApplicantId = 1,
            Username = "fellow",
            Password = "weak",
            Role = "Fellow",
            CreatedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password must be at least 8 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Role_Is_Empty()
    {
        var command = new CreateUserAccountCommand
        {
            ApplicantId = 1,
            Username = "fellow",
            Password = "Strong1!",
            Role = "",
            CreatedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Role)
            .WithErrorMessage("Role is required.");
    }

    [Fact]
    public void Should_Have_Error_When_CreatedBy_Is_Zero()
    {
        var command = new CreateUserAccountCommand
        {
            ApplicantId = 1,
            Username = "fellow",
            Password = "Strong1!",
            Role = "Fellow",
            CreatedBy = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CreatedBy)
            .WithErrorMessage("Created by must be greater than zero.");
    }

    [Fact]
    public void Should_Be_Valid_When_All_Fields_Provided()
    {
        var command = new CreateUserAccountCommand
        {
            ApplicantId = 1,
            Username = "fellow",
            Password = "Strong1!",
            Role = "Fellow",
            CreatedBy = 2
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}