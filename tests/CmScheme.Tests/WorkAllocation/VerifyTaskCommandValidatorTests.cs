using CmScheme.WorkAllocation.Application.Features.TaskVerification.VerifyTask;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.WorkAllocation;

public class VerifyTaskCommandValidatorTests
{
    private readonly VerifyTaskCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_WorkAllocationId_Is_Zero()
    {
        var command = new VerifyTaskCommand
        {
            WorkAllocationId = 0,
            VerificationStatus = "Approved",
            Comments = "All surveys verified."
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkAllocationId)
            .WithErrorMessage("WorkAllocationId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_WorkAllocationId_Is_Negative()
    {
        var command = new VerifyTaskCommand
        {
            WorkAllocationId = -4,
            VerificationStatus = "Approved",
            Comments = "All surveys verified."
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkAllocationId)
            .WithErrorMessage("WorkAllocationId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_VerificationStatus_Is_Empty()
    {
        var command = new VerifyTaskCommand
        {
            WorkAllocationId = 1,
            VerificationStatus = "",
            Comments = "All surveys verified."
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.VerificationStatus)
            .WithErrorMessage("VerificationStatus must be Pending, Approved, or Rejected.");
    }

    [Fact]
    public void Should_Have_Error_When_VerificationStatus_Is_Not_Allowed()
    {
        var command = new VerifyTaskCommand
        {
            WorkAllocationId = 1,
            VerificationStatus = "Validated",
            Comments = "All surveys verified."
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.VerificationStatus)
            .WithErrorMessage("VerificationStatus must be Pending, Approved, or Rejected.");
    }

    [Fact]
    public void Should_Have_Error_When_Comments_Exceeds_MaximumLength()
    {
        var command = new VerifyTaskCommand
        {
            WorkAllocationId = 1,
            VerificationStatus = "Approved",
            Comments = new string('C', 501)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Comments)
            .WithErrorMessage("Comments must not exceed 500 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Comments_Is_Null()
    {
        var command = new VerifyTaskCommand
        {
            WorkAllocationId = 1,
            VerificationStatus = "Rejected",
            Comments = null
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Comments);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new VerifyTaskCommand
        {
            WorkAllocationId = 1,
            VerificationStatus = "Approved",
            Comments = "All surveys verified."
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}