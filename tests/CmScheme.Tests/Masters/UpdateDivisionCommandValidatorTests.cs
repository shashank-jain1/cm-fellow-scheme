using CmScheme.Masters.Application.Features.Location.Divisions.UpdateDivision;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class UpdateDivisionCommandValidatorTests
{
    private readonly UpdateDivisionCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_DivisionId_Is_Zero()
    {
        var command = new UpdateDivisionCommand
        {
            DivisionId = 0,
            StateId = 1,
            DivisionName = "Bhopal Division"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DivisionId)
            .WithErrorMessage("Valid division is required.");
    }

    [Fact]
    public void Should_Have_Error_When_StateId_Is_Zero()
    {
        var command = new UpdateDivisionCommand
        {
            DivisionId = 1,
            StateId = 0,
            DivisionName = "Bhopal Division"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StateId)
            .WithErrorMessage("Valid state is required.");
    }

    [Fact]
    public void Should_Have_Error_When_DivisionName_Is_Empty()
    {
        var command = new UpdateDivisionCommand
        {
            DivisionId = 1,
            StateId = 1,
            DivisionName = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DivisionName)
            .WithErrorMessage("Division name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_DivisionName_Exceeds_MaximumLength()
    {
        var command = new UpdateDivisionCommand
        {
            DivisionId = 1,
            StateId = 1,
            DivisionName = new string('D', 101)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DivisionName)
            .WithErrorMessage("Division name must not exceed 100 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_DivisionCode_Exceeds_MaximumLength()
    {
        var command = new UpdateDivisionCommand
        {
            DivisionId = 1,
            StateId = 1,
            DivisionName = "Bhopal Division",
            DivisionCode = new string('X', 11)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DivisionCode)
            .WithErrorMessage("Division code must not exceed 10 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdateDivisionCommand
        {
            DivisionId = 1,
            StateId = 1,
            DivisionName = "Bhopal Division",
            DivisionCode = "BPL"
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}