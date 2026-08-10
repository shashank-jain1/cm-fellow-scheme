using CmScheme.Performance.Application.Features.Performance.SelfAssessment.SubmitSelfAssessment;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Performance;

public class SubmitSelfAssessmentCommandValidatorTests
{
    private readonly SubmitSelfAssessmentCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_UserAccountId_Is_Zero()
    {
        var command = new SubmitSelfAssessmentCommand
        {
            UserAccountId = 0,
            OverallRating = 8,
            Strengths = "Good communicator."
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.UserAccountId)
            .WithErrorMessage("UserAccountId is required.");
    }

    [Fact]
    public void Should_Have_Error_When_OverallRating_Is_Below_Range()
    {
        var command = new SubmitSelfAssessmentCommand
        {
            UserAccountId = 1,
            OverallRating = 0,
            Strengths = "Good communicator."
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.OverallRating)
            .WithErrorMessage("OverallRating must be between 1 and 10.");
    }

    [Fact]
    public void Should_Have_Error_When_OverallRating_Is_Above_Range()
    {
        var command = new SubmitSelfAssessmentCommand
        {
            UserAccountId = 1,
            OverallRating = 11,
            Strengths = "Good communicator."
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.OverallRating)
            .WithErrorMessage("OverallRating must be between 1 and 10.");
    }

    [Fact]
    public void Should_Have_Error_When_Strengths_Exceeds_MaxLength()
    {
        var command = new SubmitSelfAssessmentCommand
        {
            UserAccountId = 1,
            OverallRating = 8,
            Strengths = new string('a', 1001)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Strengths)
            .WithErrorMessage("Strengths must not exceed 1000 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Improvements_Exceeds_MaxLength()
    {
        var command = new SubmitSelfAssessmentCommand
        {
            UserAccountId = 1,
            OverallRating = 8,
            Improvements = new string('b', 1001)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Improvements)
            .WithErrorMessage("Improvements must not exceed 1000 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Optional_Text_Is_Null()
    {
        var command = new SubmitSelfAssessmentCommand
        {
            UserAccountId = 1,
            OverallRating = 8
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Strengths);
        result.ShouldNotHaveValidationErrorFor(x => x.Improvements);
        result.ShouldNotHaveValidationErrorFor(x => x.GoalsAchieved);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new SubmitSelfAssessmentCommand
        {
            UserAccountId = 1,
            ReviewCycleId = 5,
            OverallRating = 8,
            Strengths = "Good communicator.",
            Improvements = "Improve technical depth.",
            GoalsAchieved = "Shipped fellowship project.",
            GoalsMissed = "Advanced certification.",
            TrainingFeedback = "Loved the mentoring sessions."
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}