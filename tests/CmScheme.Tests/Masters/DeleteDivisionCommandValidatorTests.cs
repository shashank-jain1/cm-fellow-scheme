using CmScheme.Masters.Application.Features.Location.Divisions.DeleteDivision;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class DeleteDivisionCommandValidatorTests
{
    private readonly DeleteDivisionCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_DivisionId_Is_Zero()
    {
        var command = new DeleteDivisionCommand
        {
            DivisionId = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DivisionId)
            .WithErrorMessage("Valid division is required.");
    }

    [Fact]
    public void Should_Have_Error_When_DivisionId_Is_Negative()
    {
        var command = new DeleteDivisionCommand
        {
            DivisionId = -1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DivisionId)
            .WithErrorMessage("Valid division is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_DivisionId_Is_Positive()
    {
        var command = new DeleteDivisionCommand
        {
            DivisionId = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.DivisionId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new DeleteDivisionCommand
        {
            DivisionId = 1
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}