using CmScheme.Registration.Application.Features.ExitInterview.SubmitExitInterview;
using FluentValidation.TestHelper;

namespace CmScheme.Tests.Registration;

public class SubmitExitInterviewCommandValidatorTests
{
    private readonly SubmitExitInterviewCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_UserAccountId_Is_Zero()
    {
        var command = new SubmitExitInterviewCommand
        {
            UserAccountId = 0,
            OverallExperience = 4,
            WorkEnvironment = 4,
            LearningOpportunities = 4,
            TeamCollaboration = 4
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.UserAccountId)
            .WithErrorMessage("UserAccountId is required.");
    }

    [Fact]
    public void Should_Have_Error_When_OverallExperience_Below_Range()
    {
        var command = new SubmitExitInterviewCommand
        {
            UserAccountId = 1,
            OverallExperience = 0,
            WorkEnvironment = 4,
            LearningOpportunities = 4,
            TeamCollaboration = 4
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.OverallExperience)
            .WithErrorMessage("OverallExperience must be between 1 and 5.");
    }

    [Fact]
    public void Should_Have_Error_When_WorkEnvironment_Above_Range()
    {
        var command = new SubmitExitInterviewCommand
        {
            UserAccountId = 1,
            OverallExperience = 4,
            WorkEnvironment = 6,
            LearningOpportunities = 4,
            TeamCollaboration = 4
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkEnvironment)
            .WithErrorMessage("WorkEnvironment must be between 1 and 5.");
    }

    [Fact]
    public void Should_Have_Error_When_LearningOpportunities_Out_Of_Range()
    {
        var command = new SubmitExitInterviewCommand
        {
            UserAccountId = 1,
            OverallExperience = 4,
            WorkEnvironment = 4,
            LearningOpportunities = 7,
            TeamCollaboration = 4
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.LearningOpportunities)
            .WithErrorMessage("LearningOpportunities must be between 1 and 5.");
    }

    [Fact]
    public void Should_Have_Error_When_TeamCollaboration_Out_Of_Range()
    {
        var command = new SubmitExitInterviewCommand
        {
            UserAccountId = 1,
            OverallExperience = 4,
            WorkEnvironment = 4,
            LearningOpportunities = 4,
            TeamCollaboration = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TeamCollaboration)
            .WithErrorMessage("TeamCollaboration must be between 1 and 5.");
    }

    [Fact]
    public void Should_Have_Error_When_ImprovementSuggestions_Exceeds_1000_Characters()
    {
        var command = new SubmitExitInterviewCommand
        {
            UserAccountId = 1,
            OverallExperience = 4,
            WorkEnvironment = 4,
            LearningOpportunities = 4,
            TeamCollaboration = 4,
            ImprovementSuggestions = new string('x', 1001)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ImprovementSuggestions)
            .WithErrorMessage("ImprovementSuggestions must not exceed 1000 characters.");
    }

    [Fact]
    public void Should_Be_Valid_When_All_Fields_Provided()
    {
        var command = new SubmitExitInterviewCommand
        {
            UserAccountId = 1,
            OverallExperience = 4,
            WorkEnvironment = 4,
            LearningOpportunities = 4,
            TeamCollaboration = 4,
            ImprovementSuggestions = "More training would help.",
            WhatWorkedWell = "Supportive mentors."
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}