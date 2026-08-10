using CmScheme.Masters.Application.Features.Location.Districts.CreateDistrict;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class CreateDistrictCommandValidatorTests
{
    private readonly CreateDistrictCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_DivisionId_Is_Zero()
    {
        var command = new CreateDistrictCommand
        {
            DivisionId = 0,
            DistrictName = "Bhopal District"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DivisionId)
            .WithErrorMessage("Valid division is required.");
    }

    [Fact]
    public void Should_Have_Error_When_DistrictName_Is_Empty()
    {
        var command = new CreateDistrictCommand
        {
            DivisionId = 1,
            DistrictName = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DistrictName)
            .WithErrorMessage("District name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_DistrictName_Exceeds_MaximumLength()
    {
        var command = new CreateDistrictCommand
        {
            DivisionId = 1,
            DistrictName = new string('D', 101)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DistrictName)
            .WithErrorMessage("District name must not exceed 100 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_DistrictCode_Exceeds_MaximumLength()
    {
        var command = new CreateDistrictCommand
        {
            DivisionId = 1,
            DistrictName = "Bhopal District",
            DistrictCode = new string('X', 11)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DistrictCode)
            .WithErrorMessage("District code must not exceed 10 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_DistrictCode_Is_Empty()
    {
        var command = new CreateDistrictCommand
        {
            DivisionId = 1,
            DistrictName = "Bhopal District",
            DistrictCode = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.DistrictCode);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateDistrictCommand
        {
            DivisionId = 1,
            DistrictName = "Bhopal District",
            DistrictCode = "BPL"
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}