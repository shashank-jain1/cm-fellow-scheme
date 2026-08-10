using CmScheme.Certificate.Application.Features.Exit.VerifyExitCompliance;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Certificate;

public class VerifyExitComplianceCommandValidatorTests
{
    private readonly VerifyExitComplianceCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ExitRecordId_Is_Zero()
    {
        var command = new VerifyExitComplianceCommand
        {
            ExitRecordId = 0,
            Status = "Compliant",
            VerifiedBy = "Compliance Officer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ExitRecordId)
            .WithErrorMessage("ExitRecordId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_Status_Is_Empty()
    {
        var command = new VerifyExitComplianceCommand
        {
            ExitRecordId = 1,
            Status = "",
            VerifiedBy = "Compliance Officer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("Status is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Status_Exceeds_MaxLength()
    {
        var command = new VerifyExitComplianceCommand
        {
            ExitRecordId = 1,
            Status = new string('s', 51),
            VerifiedBy = "Compliance Officer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("Status must not exceed 50 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_VerifiedBy_Is_Empty()
    {
        var command = new VerifyExitComplianceCommand
        {
            ExitRecordId = 1,
            Status = "Compliant",
            VerifiedBy = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.VerifiedBy)
            .WithErrorMessage("VerifiedBy is required.");
    }

    [Fact]
    public void Should_Have_Error_When_VerifiedBy_Exceeds_MaxLength()
    {
        var command = new VerifyExitComplianceCommand
        {
            ExitRecordId = 1,
            Status = "Compliant",
            VerifiedBy = new string('v', 201)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.VerifiedBy)
            .WithErrorMessage("VerifiedBy must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new VerifyExitComplianceCommand
        {
            ExitRecordId = 1,
            Status = "Compliant",
            VerifiedBy = "Compliance Officer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}