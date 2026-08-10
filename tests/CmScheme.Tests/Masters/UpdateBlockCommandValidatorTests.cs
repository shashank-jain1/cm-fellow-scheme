using CmScheme.Masters.Application.Features.Location.Blocks.UpdateBlock;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class UpdateBlockCommandValidatorTests
{
    private readonly UpdateBlockCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_BlockId_Is_Zero()
    {
        var command = new UpdateBlockCommand
        {
            BlockId = 0,
            DistrictId = 1,
            BlockName = "Berasia Block"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.BlockId)
            .WithErrorMessage("Valid block is required.");
    }

    [Fact]
    public void Should_Have_Error_When_DistrictId_Is_Zero()
    {
        var command = new UpdateBlockCommand
        {
            BlockId = 1,
            DistrictId = 0,
            BlockName = "Berasia Block"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DistrictId)
            .WithErrorMessage("Valid district is required.");
    }

    [Fact]
    public void Should_Have_Error_When_BlockName_Is_Empty()
    {
        var command = new UpdateBlockCommand
        {
            BlockId = 1,
            DistrictId = 1,
            BlockName = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.BlockName)
            .WithErrorMessage("Block name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_BlockName_Exceeds_MaximumLength()
    {
        var command = new UpdateBlockCommand
        {
            BlockId = 1,
            DistrictId = 1,
            BlockName = new string('B', 101)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.BlockName)
            .WithErrorMessage("Block name must not exceed 100 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_BlockCode_Exceeds_MaximumLength()
    {
        var command = new UpdateBlockCommand
        {
            BlockId = 1,
            DistrictId = 1,
            BlockName = "Berasia Block",
            BlockCode = new string('X', 11)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.BlockCode)
            .WithErrorMessage("Block code must not exceed 10 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdateBlockCommand
        {
            BlockId = 1,
            DistrictId = 1,
            BlockName = "Berasia Block",
            BlockCode = "BR"
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}