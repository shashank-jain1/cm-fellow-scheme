using CmScheme.Performance.Application.Features.Performance.RecordEvaluationRemarks;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Performance;

public class RecordEvaluationRemarksCommandValidatorTests
{
    private readonly RecordEvaluationRemarksCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_PerformanceEvaluationId_Is_Zero()
    {
        var command = new RecordEvaluationRemarksCommand
        {
            PerformanceEvaluationId = 0,
            EvaluationRemarks = "Good performance.",
            EvaluatedBy = "Supervisor"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PerformanceEvaluationId)
            .WithErrorMessage("PerformanceEvaluationId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_EvaluationRemarks_Is_Empty()
    {
        var command = new RecordEvaluationRemarksCommand
        {
            PerformanceEvaluationId = 1,
            EvaluationRemarks = "",
            EvaluatedBy = "Supervisor"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EvaluationRemarks)
            .WithErrorMessage("EvaluationRemarks is required.");
    }

    [Fact]
    public void Should_Have_Error_When_EvaluationRemarks_Exceeds_MaxLength()
    {
        var command = new RecordEvaluationRemarksCommand
        {
            PerformanceEvaluationId = 1,
            EvaluationRemarks = new string('a', 2001),
            EvaluatedBy = "Supervisor"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EvaluationRemarks)
            .WithErrorMessage("EvaluationRemarks must not exceed 2000 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_EvaluatedBy_Is_Empty()
    {
        var command = new RecordEvaluationRemarksCommand
        {
            PerformanceEvaluationId = 1,
            EvaluationRemarks = "Good performance.",
            EvaluatedBy = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EvaluatedBy)
            .WithErrorMessage("EvaluatedBy is required.");
    }

    [Fact]
    public void Should_Have_Error_When_EvaluatedBy_Exceeds_MaxLength()
    {
        var command = new RecordEvaluationRemarksCommand
        {
            PerformanceEvaluationId = 1,
            EvaluationRemarks = "Good performance.",
            EvaluatedBy = new string('b', 201)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EvaluatedBy)
            .WithErrorMessage("EvaluatedBy must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new RecordEvaluationRemarksCommand
        {
            PerformanceEvaluationId = 1,
            EvaluationRemarks = "Good performance.",
            EvaluatedBy = "Supervisor"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}