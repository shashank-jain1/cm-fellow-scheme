using CmScheme.Masters.Application.Features.Location.Blocks.DeleteBlock;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class DeleteBlockCommandValidatorTests
{
    private readonly DeleteBlockCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_BlockId_Is_Zero()
    {
        var command = new DeleteBlockCommand
        {
            BlockId = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.BlockId)
            .WithErrorMessage("Valid block is required.");
    }

    [Fact]
    public void Should_Have_Error_When_BlockId_Is_Negative()
    {
        var command = new DeleteBlockCommand
        {
            BlockId = -1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.BlockId)
            .WithErrorMessage("Valid block is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_BlockId_Is_Positive()
    {
        var command = new DeleteBlockCommand
        {
            BlockId = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.BlockId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new DeleteBlockCommand
        {
            BlockId = 1
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}