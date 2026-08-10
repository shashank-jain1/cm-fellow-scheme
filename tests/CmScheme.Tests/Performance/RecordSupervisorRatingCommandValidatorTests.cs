using CmScheme.Performance.Application.Features.Performance.RecordSupervisorRating;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Performance;

public class RecordSupervisorRatingCommandValidatorTests
{
    private readonly RecordSupervisorRatingCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_PerformanceEvaluationId_Is_Zero()
    {
        var command = new RecordSupervisorRatingCommand
        {
            PerformanceEvaluationId = 0,
            SupervisorRating = 8m,
            EvaluatedBy = "Supervisor"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PerformanceEvaluationId)
            .WithErrorMessage("PerformanceEvaluationId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_SupervisorRating_Is_Below_Range()
    {
        var command = new RecordSupervisorRatingCommand
        {
            PerformanceEvaluationId = 1,
            SupervisorRating = 0m,
            EvaluatedBy = "Supervisor"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.SupervisorRating)
            .WithErrorMessage("SupervisorRating must be between 1 and 10.");
    }

    [Fact]
    public void Should_Have_Error_When_SupervisorRating_Is_Above_Range()
    {
        var command = new RecordSupervisorRatingCommand
        {
            PerformanceEvaluationId = 1,
            SupervisorRating = 11m,
            EvaluatedBy = "Supervisor"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.SupervisorRating)
            .WithErrorMessage("SupervisorRating must be between 1 and 10.");
    }

    [Fact]
    public void Should_Have_Error_When_EvaluatedBy_Is_Empty()
    {
        var command = new RecordSupervisorRatingCommand
        {
            PerformanceEvaluationId = 1,
            SupervisorRating = 8m,
            EvaluatedBy = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EvaluatedBy)
            .WithErrorMessage("EvaluatedBy is required.");
    }

    [Fact]
    public void Should_Have_Error_When_EvaluatedBy_Exceeds_MaxLength()
    {
        var command = new RecordSupervisorRatingCommand
        {
            PerformanceEvaluationId = 1,
            SupervisorRating = 8m,
            EvaluatedBy = new string('b', 201)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EvaluatedBy)
            .WithErrorMessage("EvaluatedBy must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new RecordSupervisorRatingCommand
        {
            PerformanceEvaluationId = 1,
            SupervisorRating = 8m,
            EvaluatedBy = "Supervisor"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}