using CmScheme.Masters.Application.Features.Location.States.CreateState;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class CreateStateCommandValidatorTests
{
    private readonly CreateStateCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_StateName_Is_Empty()
    {
        var command = new CreateStateCommand
        {
            StateName = "",
            StateCode = "ST01"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StateName)
            .WithErrorMessage("State name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_StateName_Exceeds_MaximumLength()
    {
        var command = new CreateStateCommand
        {
            StateName = new string('A', 101),
            StateCode = "ST01"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StateName)
            .WithErrorMessage("State name must not exceed 100 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_StateCode_Is_Empty()
    {
        var command = new CreateStateCommand
        {
            StateName = "Test State",
            StateCode = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StateCode)
            .WithErrorMessage("State code is required.");
    }

    [Fact]
    public void Should_Have_Error_When_StateCode_Exceeds_MaximumLength()
    {
        var command = new CreateStateCommand
        {
            StateName = "Test State",
            StateCode = new string('C', 11)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StateCode)
            .WithErrorMessage("State code must not exceed 10 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_StateShortName_Is_Empty()
    {
        var command = new CreateStateCommand
        {
            StateName = "Test State",
            StateCode = "ST01",
            StateShortName = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.StateShortName);
    }

    [Fact]
    public void Should_Have_Error_When_StateShortName_Exceeds_MaximumLength()
    {
        var command = new CreateStateCommand
        {
            StateName = "Test State",
            StateCode = "ST01",
            StateShortName = new string('S', 21)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StateShortName)
            .WithErrorMessage("State short name must not exceed 20 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateStateCommand
        {
            StateName = "Madhya Pradesh",
            StateCode = "MP",
            StateShortName = "MP",
            DisplayOrder = 1
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}