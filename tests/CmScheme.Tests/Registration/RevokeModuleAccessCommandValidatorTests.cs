using CmScheme.Registration.Application.Features.UserModuleAccess.RevokeModuleAccess;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class RevokeModuleAccessCommandValidatorTests
{
    private readonly RevokeModuleAccessCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_UserModuleAccessId_Is_Zero()
    {
        var command = new RevokeModuleAccessCommand
        {
            UserModuleAccessId = 0,
            PerformedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.UserModuleAccessId)
            .WithErrorMessage("UserModuleAccessId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_PerformedBy_Is_Zero()
    {
        var command = new RevokeModuleAccessCommand
        {
            UserModuleAccessId = 1,
            PerformedBy = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PerformedBy)
            .WithErrorMessage("PerformedBy must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_Both_Ids_Are_Zero()
    {
        var command = new RevokeModuleAccessCommand
        {
            UserModuleAccessId = 0,
            PerformedBy = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.UserModuleAccessId);
        result.ShouldHaveValidationErrorFor(x => x.PerformedBy);
    }

    [Fact]
    public void Should_Be_Valid_When_All_Fields_Provided()
    {
        var command = new RevokeModuleAccessCommand
        {
            UserModuleAccessId = 1,
            PerformedBy = 2
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}