using CmScheme.Masters.Application.Features.LookupMaster.CreateLookupMaster;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class CreateLookupMasterCommandValidatorTests
{
    private readonly CreateLookupMasterCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_MasterType_Is_Empty()
    {
        var command = new CreateLookupMasterCommand
        {
            MasterType = "",
            Label = "Male",
            Value = "M"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MasterType)
            .WithErrorMessage("MasterType is required.");
    }

    [Fact]
    public void Should_Have_Error_When_MasterType_Exceeds_MaximumLength()
    {
        var command = new CreateLookupMasterCommand
        {
            MasterType = new string('I', 51),
            Label = "Male",
            Value = "M"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MasterType)
            .WithErrorMessage("MasterType must not exceed 50 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Label_Is_Empty()
    {
        var command = new CreateLookupMasterCommand
        {
            MasterType = "Gender",
            Label = "",
            Value = "M"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Label)
            .WithErrorMessage("Label is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Value_Exceeds_MaximumLength()
    {
        var command = new CreateLookupMasterCommand
        {
            MasterType = "Gender",
            Label = "Male",
            Value = new string('V', 101)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Value)
            .WithErrorMessage("Value must not exceed 100 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateLookupMasterCommand
        {
            MasterType = "Gender",
            Label = "Male",
            Value = "M",
            SortOrder = 1
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}