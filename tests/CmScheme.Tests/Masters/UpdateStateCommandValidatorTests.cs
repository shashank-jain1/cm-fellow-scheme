using CmScheme.Masters.Application.Features.Location.States.UpdateState;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class UpdateStateCommandValidatorTests
{
    private readonly UpdateStateCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_StateId_Is_Zero()
    {
        var command = new UpdateStateCommand
        {
            StateId = 0,
            StateName = "Madhya Pradesh",
            StateCode = "MP"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StateId)
            .WithErrorMessage("StateId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_StateName_Is_Empty()
    {
        var command = new UpdateStateCommand
        {
            StateId = 1,
            StateName = "",
            StateCode = "MP"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StateName)
            .WithErrorMessage("State name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_StateName_Exceeds_MaximumLength()
    {
        var command = new UpdateStateCommand
        {
            StateId = 1,
            StateName = new string('A', 101),
            StateCode = "MP"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StateName)
            .WithErrorMessage("State name must not exceed 100 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_StateCode_Exceeds_MaximumLength()
    {
        var command = new UpdateStateCommand
        {
            StateId = 1,
            StateName = "Madhya Pradesh",
            StateCode = new string('C', 11)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StateCode)
            .WithErrorMessage("State code must not exceed 10 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_StateShortName_Is_Null()
    {
        var command = new UpdateStateCommand
        {
            StateId = 1,
            StateName = "Madhya Pradesh",
            StateCode = "MP",
            StateShortName = null
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.StateShortName);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdateStateCommand
        {
            StateId = 1,
            StateName = "Madhya Pradesh",
            StateCode = "MP",
            StateShortName = "MP",
            IsActive = true
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}