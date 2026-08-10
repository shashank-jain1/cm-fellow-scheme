using CmScheme.Registration.Application.Features.UserModuleAccess.GrantModuleAccess;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class GrantModuleAccessCommandValidatorTests
{
    private readonly GrantModuleAccessCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_UserAccountId_Is_Zero()
    {
        var command = new GrantModuleAccessCommand
        {
            UserAccountId = 0,
            ModuleMasterId = 1,
            PerformedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.UserAccountId)
            .WithErrorMessage("User account ID must be greater than zero.");
    }

    [Fact]
    public void Should_Have_Error_When_ModuleMasterId_Is_Zero()
    {
        var command = new GrantModuleAccessCommand
        {
            UserAccountId = 1,
            ModuleMasterId = 0,
            PerformedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ModuleMasterId)
            .WithErrorMessage("Module master ID must be greater than zero.");
    }

    [Fact]
    public void Should_Have_Error_When_PerformedBy_Is_Zero()
    {
        var command = new GrantModuleAccessCommand
        {
            UserAccountId = 1,
            ModuleMasterId = 1,
            PerformedBy = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PerformedBy)
            .WithErrorMessage("Performed by must be greater than zero.");
    }

    [Fact]
    public void Should_Be_Valid_When_All_Fields_Provided()
    {
        var command = new GrantModuleAccessCommand
        {
            UserAccountId = 1,
            ModuleMasterId = 1,
            CanRead = true,
            CanWrite = true,
            PerformedBy = 2
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}