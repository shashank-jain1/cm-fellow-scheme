using CmScheme.Registration.Application.Features.Registration.ApproveRegistration;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class ApproveRegistrationCommandValidatorTests
{
    private readonly ApproveRegistrationCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ApplicantId_Is_Zero()
    {
        var command = new ApproveRegistrationCommand
        {
            ApplicantId = 0,
            ApprovedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId)
            .WithErrorMessage("Applicant ID must be greater than zero.");
    }

    [Fact]
    public void Should_Have_Error_When_ApplicantId_Is_Negative()
    {
        var command = new ApproveRegistrationCommand
        {
            ApplicantId = -5,
            ApprovedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId);
    }

    [Fact]
    public void Should_Have_Error_When_ApprovedBy_Is_Zero()
    {
        var command = new ApproveRegistrationCommand
        {
            ApplicantId = 1,
            ApprovedBy = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApprovedBy)
            .WithErrorMessage("Approved by must be greater than zero.");
    }

    [Fact]
    public void Should_Be_Valid_When_ApplicantId_And_ApprovedBy_Provided()
    {
        var command = new ApproveRegistrationCommand
        {
            ApplicantId = 1,
            ApprovedBy = 2
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}