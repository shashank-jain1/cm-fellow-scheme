using CmScheme.Certificate.Application.Features.Certificates.VerifyCertificate;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Certificate;

public class VerifyCertificateQueryValidatorTests
{
    private readonly VerifyCertificateQueryValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_CertificateNumber_Is_Empty()
    {
        var query = new VerifyCertificateQuery
        {
            CertificateNumber = ""
        };

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.CertificateNumber)
            .WithErrorMessage("Certificate number is required.");
    }

    [Fact]
    public void Should_Have_Error_When_CertificateNumber_Is_Whitespace()
    {
        var query = new VerifyCertificateQuery
        {
            CertificateNumber = "   "
        };

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.CertificateNumber)
            .WithErrorMessage("Certificate number is required.");
    }

    [Fact]
    public void Should_Have_Error_When_CertificateNumber_Exceeds_MaxLength()
    {
        var query = new VerifyCertificateQuery
        {
            CertificateNumber = new string('c', 101)
        };

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.CertificateNumber)
            .WithErrorMessage("Certificate number must not exceed 100 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_CertificateNumber_Is_Provided()
    {
        var query = new VerifyCertificateQuery
        {
            CertificateNumber = "CM-2026-0042"
        };

        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }
}