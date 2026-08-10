using CmScheme.Certificate.Application.Features.Exit.SubmitExitReadiness;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Certificate;

public class SubmitExitReadinessCommandValidatorTests
{
    private readonly SubmitExitReadinessCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ApplicantId_Is_Zero()
    {
        var command = new SubmitExitReadinessCommand
        {
            ApplicantId = 0,
            CompletionStatus = "Ready",
            VerificationFlags = "All clear",
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId)
            .WithErrorMessage("ApplicantId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_CompletionStatus_Is_Empty()
    {
        var command = new SubmitExitReadinessCommand
        {
            ApplicantId = 1,
            CompletionStatus = "",
            VerificationFlags = "All clear",
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CompletionStatus)
            .WithErrorMessage("CompletionStatus is required.");
    }

    [Fact]
    public void Should_Have_Error_When_CompletionStatus_Exceeds_MaxLength()
    {
        var command = new SubmitExitReadinessCommand
        {
            ApplicantId = 1,
            CompletionStatus = new string('c', 51),
            VerificationFlags = "All clear",
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CompletionStatus)
            .WithErrorMessage("CompletionStatus must not exceed 50 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_VerificationFlags_Is_Empty()
    {
        var command = new SubmitExitReadinessCommand
        {
            ApplicantId = 1,
            CompletionStatus = "Ready",
            VerificationFlags = "",
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.VerificationFlags)
            .WithErrorMessage("VerificationFlags is required.");
    }

    [Fact]
    public void Should_Have_Error_When_VerificationFlags_Exceeds_MaxLength()
    {
        var command = new SubmitExitReadinessCommand
        {
            ApplicantId = 1,
            CompletionStatus = "Ready",
            VerificationFlags = new string('f', 501),
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.VerificationFlags)
            .WithErrorMessage("VerificationFlags must not exceed 500 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_CreatedBy_Is_Empty()
    {
        var command = new SubmitExitReadinessCommand
        {
            ApplicantId = 1,
            CompletionStatus = "Ready",
            VerificationFlags = "All clear",
            CreatedBy = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CreatedBy)
            .WithErrorMessage("CreatedBy is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new SubmitExitReadinessCommand
        {
            ApplicantId = 1,
            CompletionStatus = "Ready",
            VerificationFlags = "All clear",
            ExitReportPath = "/reports/exit-1.pdf",
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}