using CmScheme.Registration.Application.Features.UserModuleAccess.BulkUpdateModuleAccess;
using CmScheme.Registration.Core.Dtos;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class BulkUpdateModuleAccessCommandValidatorTests
{
    private readonly BulkUpdateModuleAccessCommandValidator _validator = new();

    private static ModuleAccessItemDto ValidAccess() => new()
    {
        ModuleMasterId = 1,
        ModuleCode = "M001",
        ModuleName = "Registration"
    };

    [Fact]
    public void Should_Have_Error_When_UserAccountId_Is_Zero()
    {
        var command = new BulkUpdateModuleAccessCommand
        {
            UserAccountId = 0,
            Accesses = [ValidAccess()],
            PerformedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.UserAccountId)
            .WithErrorMessage("User account ID must be greater than zero.");
    }

    [Fact]
    public void Should_Have_Error_When_Accesses_Is_Empty()
    {
        var command = new BulkUpdateModuleAccessCommand
        {
            UserAccountId = 1,
            Accesses = [],
            PerformedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Accesses)
            .WithErrorMessage("Accesses list must not be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_PerformedBy_Is_Zero()
    {
        var command = new BulkUpdateModuleAccessCommand
        {
            UserAccountId = 1,
            Accesses = [ValidAccess()],
            PerformedBy = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PerformedBy)
            .WithErrorMessage("Performed by must be greater than zero.");
    }

    [Fact]
    public void Should_Have_Error_When_Access_ModuleMasterId_Is_Zero()
    {
        var command = new BulkUpdateModuleAccessCommand
        {
            UserAccountId = 1,
            Accesses = [ValidAccess() with { ModuleMasterId = 0 }],
            PerformedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor("Accesses[0].ModuleMasterId")
            .WithErrorMessage("Module master ID must be greater than zero.");
    }

    [Fact]
    public void Should_Have_Error_When_Access_ModuleCode_Is_Empty()
    {
        var command = new BulkUpdateModuleAccessCommand
        {
            UserAccountId = 1,
            Accesses = [ValidAccess() with { ModuleCode = "" }],
            PerformedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor("Accesses[0].ModuleCode")
            .WithErrorMessage("Module code is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Access_ModuleName_Is_Empty()
    {
        var command = new BulkUpdateModuleAccessCommand
        {
            UserAccountId = 1,
            Accesses = [ValidAccess() with { ModuleName = "" }],
            PerformedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor("Accesses[0].ModuleName")
            .WithErrorMessage("Module name is required.");
    }

    [Fact]
    public void Should_Be_Valid_When_All_Fields_Provided()
    {
        var command = new BulkUpdateModuleAccessCommand
        {
            UserAccountId = 1,
            Accesses = [ValidAccess()],
            PerformedBy = 2
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}