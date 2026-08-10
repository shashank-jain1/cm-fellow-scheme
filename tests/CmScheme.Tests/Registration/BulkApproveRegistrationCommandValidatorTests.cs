using CmScheme.Registration.Application.Features.Registration.BulkApprove;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class BulkApproveRegistrationCommandValidatorTests
{
    private readonly BulkApproveRegistrationCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ApplicantIds_Is_Empty()
    {
        var command = new BulkApproveRegistrationCommand
        {
            ApplicantIds = []
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantIds)
            .WithErrorMessage("ApplicantIds is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ApplicantIds_Is_Null()
    {
        var command = new BulkApproveRegistrationCommand
        {
            ApplicantIds = null!
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantIds)
            .WithErrorMessage("ApplicantIds is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_ApplicantIds_Is_NotEmpty()
    {
        var command = new BulkApproveRegistrationCommand
        {
            ApplicantIds = [1, 2, 3]
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.ApplicantIds);
    }

    [Fact]
    public void Should_Be_Valid_When_ApplicantIds_Provided()
    {
        var command = new BulkApproveRegistrationCommand
        {
            ApplicantIds = [101, 202]
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}