using CmScheme.Masters.Application.Features.Location.Districts.DeleteDistrict;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class DeleteDistrictCommandValidatorTests
{
    private readonly DeleteDistrictCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_DistrictId_Is_Zero()
    {
        var command = new DeleteDistrictCommand
        {
            DistrictId = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DistrictId)
            .WithErrorMessage("Valid district is required.");
    }

    [Fact]
    public void Should_Have_Error_When_DistrictId_Is_Negative()
    {
        var command = new DeleteDistrictCommand
        {
            DistrictId = -1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DistrictId)
            .WithErrorMessage("Valid district is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_DistrictId_Is_Positive()
    {
        var command = new DeleteDistrictCommand
        {
            DistrictId = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.DistrictId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new DeleteDistrictCommand
        {
            DistrictId = 1
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}