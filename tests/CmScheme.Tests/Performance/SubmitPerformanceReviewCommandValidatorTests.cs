using CmScheme.Performance.Application.Features.Performance.SubmitPerformanceReview;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Performance;

public class SubmitPerformanceReviewCommandValidatorTests
{
    private readonly SubmitPerformanceReviewCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_PerformanceEvaluationId_Is_Zero()
    {
        var command = new SubmitPerformanceReviewCommand
        {
            PerformanceEvaluationId = 0,
            Action = "Submit",
            PerformedBy = "Manager"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PerformanceEvaluationId)
            .WithErrorMessage("PerformanceEvaluationId is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Action_Is_Empty()
    {
        var command = new SubmitPerformanceReviewCommand
        {
            PerformanceEvaluationId = 1,
            Action = "",
            PerformedBy = "Manager"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Action)
            .WithErrorMessage("Action is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Action_Is_Not_Allowed()
    {
        var command = new SubmitPerformanceReviewCommand
        {
            PerformanceEvaluationId = 1,
            Action = "Delete",
            PerformedBy = "Manager"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Action)
            .WithErrorMessage("Action must be Submit, Approve, or Reject.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Action_Is_Allowed_CaseInsensitively()
    {
        var command = new SubmitPerformanceReviewCommand
        {
            PerformanceEvaluationId = 1,
            Action = "approve",
            PerformedBy = "Manager"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Action);
    }

    [Fact]
    public void Should_Have_Error_When_PerformedBy_Is_Empty()
    {
        var command = new SubmitPerformanceReviewCommand
        {
            PerformanceEvaluationId = 1,
            Action = "Submit",
            PerformedBy = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PerformedBy)
            .WithErrorMessage("PerformedBy is required.");
    }

    [Fact]
    public void Should_Have_Error_When_PerformedBy_Exceeds_MaxLength()
    {
        var command = new SubmitPerformanceReviewCommand
        {
            PerformanceEvaluationId = 1,
            Action = "Submit",
            PerformedBy = new string('m', 201)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PerformedBy)
            .WithErrorMessage("PerformedBy must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Remarks_Exceeds_MaxLength()
    {
        var command = new SubmitPerformanceReviewCommand
        {
            PerformanceEvaluationId = 1,
            Action = "Submit",
            PerformedBy = "Manager",
            Remarks = new string('r', 2001)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Remarks)
            .WithErrorMessage("Remarks must not exceed 2000 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new SubmitPerformanceReviewCommand
        {
            PerformanceEvaluationId = 1,
            Action = "Submit",
            PerformedBy = "Manager",
            Remarks = "Looks good."
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}