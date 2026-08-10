using CmScheme.Registration.Application.Features.UserAccount.AssignRole;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class AssignRoleCommandValidatorTests
{
    private readonly AssignRoleCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_UserAccountId_Is_Zero()
    {
        var command = new AssignRoleCommand
        {
            UserAccountId = 0,
            Role = "Admin",
            ModifiedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.UserAccountId)
            .WithErrorMessage("User account ID must be greater than zero.");
    }

    [Fact]
    public void Should_Have_Error_When_Role_Is_Empty()
    {
        var command = new AssignRoleCommand
        {
            UserAccountId = 1,
            Role = "",
            ModifiedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Role)
            .WithErrorMessage("Role is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Role_Exceeds_50_Characters()
    {
        var command = new AssignRoleCommand
        {
            UserAccountId = 1,
            Role = new string('r', 51),
            ModifiedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Role)
            .WithErrorMessage("Role must not exceed 50 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_ModifiedBy_Is_Zero()
    {
        var command = new AssignRoleCommand
        {
            UserAccountId = 1,
            Role = "Admin",
            ModifiedBy = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ModifiedBy)
            .WithErrorMessage("Modified by must be greater than zero.");
    }

    [Fact]
    public void Should_Be_Valid_When_All_Fields_Provided()
    {
        var command = new AssignRoleCommand
        {
            UserAccountId = 1,
            Role = "Admin",
            ModifiedBy = 2
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}