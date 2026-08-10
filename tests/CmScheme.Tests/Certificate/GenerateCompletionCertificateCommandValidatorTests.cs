using CmScheme.Certificate.Application.Features.Certificates.GenerateCompletionCertificate;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Certificate;

public class GenerateCompletionCertificateCommandValidatorTests
{
    private readonly GenerateCompletionCertificateCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ApplicantId_Is_Zero()
    {
        var command = new GenerateCompletionCertificateCommand
        {
            ApplicantId = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId)
            .WithErrorMessage("Applicant ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ApplicantId_Is_Negative()
    {
        var command = new GenerateCompletionCertificateCommand
        {
            ApplicantId = -3
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_ApplicantId_Is_Positive()
    {
        var command = new GenerateCompletionCertificateCommand
        {
            ApplicantId = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.ApplicantId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new GenerateCompletionCertificateCommand
        {
            ApplicantId = 75
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}