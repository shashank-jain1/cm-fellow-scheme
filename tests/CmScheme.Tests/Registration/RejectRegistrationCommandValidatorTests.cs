using CmScheme.Registration.Application.Features.Registration.RejectRegistration;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class RejectRegistrationCommandValidatorTests
{
    private readonly RejectRegistrationCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ApplicantId_Is_Zero()
    {
        var command = new RejectRegistrationCommand
        {
            ApplicantId = 0,
            Reason = "Invalid documents",
            RejectedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId)
            .WithErrorMessage("Applicant ID must be greater than zero.");
    }

    [Fact]
    public void Should_Have_Error_When_Reason_Is_Empty()
    {
        var command = new RejectRegistrationCommand
        {
            ApplicantId = 1,
            Reason = "",
            RejectedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Reason)
            .WithErrorMessage("Rejection reason is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Reason_Exceeds_500_Characters()
    {
        var command = new RejectRegistrationCommand
        {
            ApplicantId = 1,
            Reason = new string('x', 501),
            RejectedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Reason)
            .WithErrorMessage("Rejection reason must not exceed 500 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_RejectedBy_Is_Zero()
    {
        var command = new RejectRegistrationCommand
        {
            ApplicantId = 1,
            Reason = "Invalid documents",
            RejectedBy = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.RejectedBy)
            .WithErrorMessage("Rejected by must be greater than zero.");
    }

    [Fact]
    public void Should_Be_Valid_When_All_Fields_Provided()
    {
        var command = new RejectRegistrationCommand
        {
            ApplicantId = 1,
            Reason = "Invalid documents",
            RejectedBy = 2
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}