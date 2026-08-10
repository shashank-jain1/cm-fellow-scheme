using CmScheme.Certificate.Application.Features.Certificate.GenerateCertificate;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Certificate;

public class GenerateCertificateCommandValidatorTests
{
    private readonly GenerateCertificateCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_CertificateId_Is_Zero()
    {
        var command = new GenerateCertificateCommand
        {
            CertificateId = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CertificateId)
            .WithErrorMessage("Certificate ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_CertificateId_Is_Negative()
    {
        var command = new GenerateCertificateCommand
        {
            CertificateId = -10
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CertificateId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_CertificateId_Is_Positive()
    {
        var command = new GenerateCertificateCommand
        {
            CertificateId = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.CertificateId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new GenerateCertificateCommand
        {
            CertificateId = 500
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}