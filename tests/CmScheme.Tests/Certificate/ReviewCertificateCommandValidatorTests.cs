using CmScheme.Certificate.Application.Features.Certificate.ReviewCertificate;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Certificate;

public class ReviewCertificateCommandValidatorTests
{
    private readonly ReviewCertificateCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_CertificateId_Is_Zero()
    {
        var command = new ReviewCertificateCommand
        {
            CertificateId = 0,
            Status = "Approved",
            VerifiedBy = "Reviewer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CertificateId)
            .WithErrorMessage("CertificateId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_Status_Is_Empty()
    {
        var command = new ReviewCertificateCommand
        {
            CertificateId = 1,
            Status = "",
            VerifiedBy = "Reviewer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("Status is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Status_Exceeds_MaxLength()
    {
        var command = new ReviewCertificateCommand
        {
            CertificateId = 1,
            Status = new string('s', 51),
            VerifiedBy = "Reviewer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("Status must not exceed 50 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_VerifiedBy_Is_Empty()
    {
        var command = new ReviewCertificateCommand
        {
            CertificateId = 1,
            Status = "Approved",
            VerifiedBy = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.VerifiedBy)
            .WithErrorMessage("VerifiedBy is required.");
    }

    [Fact]
    public void Should_Have_Error_When_VerifiedBy_Exceeds_MaxLength()
    {
        var command = new ReviewCertificateCommand
        {
            CertificateId = 1,
            Status = "Approved",
            VerifiedBy = new string('v', 201)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.VerifiedBy)
            .WithErrorMessage("VerifiedBy must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new ReviewCertificateCommand
        {
            CertificateId = 1,
            Status = "Approved",
            VerifiedBy = "Reviewer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}