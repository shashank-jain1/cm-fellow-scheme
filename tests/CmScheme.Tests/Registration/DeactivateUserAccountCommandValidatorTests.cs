using CmScheme.Registration.Application.Features.UserAccount.DeactivateUserAccount;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class DeactivateUserAccountCommandValidatorTests
{
    private readonly DeactivateUserAccountCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_UserAccountId_Is_Zero()
    {
        var command = new DeactivateUserAccountCommand
        {
            UserAccountId = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.UserAccountId)
            .WithErrorMessage("UserAccountId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_UserAccountId_Is_Negative()
    {
        var command = new DeactivateUserAccountCommand
        {
            UserAccountId = -1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.UserAccountId);
    }

    [Fact]
    public void Should_Be_Valid_When_UserAccountId_Is_Positive()
    {
        var command = new DeactivateUserAccountCommand
        {
            UserAccountId = 1
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}