using CmScheme.Masters.Application.Features.Location.States.DeleteState;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class DeleteStateCommandValidatorTests
{
    private readonly DeleteStateCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_StateId_Is_Zero()
    {
        var command = new DeleteStateCommand
        {
            StateId = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StateId)
            .WithErrorMessage("StateId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_StateId_Is_Negative()
    {
        var command = new DeleteStateCommand
        {
            StateId = -1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StateId)
            .WithErrorMessage("StateId must be greater than 0.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_StateId_Is_Positive()
    {
        var command = new DeleteStateCommand
        {
            StateId = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.StateId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new DeleteStateCommand
        {
            StateId = 1
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}