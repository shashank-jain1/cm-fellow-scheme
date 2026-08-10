using CmScheme.Performance.Application.Features.Performance.CalculatePerformanceScore;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Performance;

public class CalculatePerformanceScoreCommandValidatorTests
{
    private readonly CalculatePerformanceScoreCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_PerformanceEvaluationId_Is_Zero()
    {
        var command = new CalculatePerformanceScoreCommand
        {
            PerformanceEvaluationId = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PerformanceEvaluationId)
            .WithErrorMessage("Performance Evaluation ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_PerformanceEvaluationId_Is_Negative()
    {
        var command = new CalculatePerformanceScoreCommand
        {
            PerformanceEvaluationId = -5
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PerformanceEvaluationId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_PerformanceEvaluationId_Is_Positive()
    {
        var command = new CalculatePerformanceScoreCommand
        {
            PerformanceEvaluationId = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.PerformanceEvaluationId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CalculatePerformanceScoreCommand
        {
            PerformanceEvaluationId = 100
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}