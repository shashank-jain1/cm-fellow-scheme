using CmScheme.Certificate.Application.Features.Certificates.GenerateExperienceLetter;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Certificate;

public class GenerateExperienceLetterCommandValidatorTests
{
    private readonly GenerateExperienceLetterCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ApplicantId_Is_Zero()
    {
        var command = new GenerateExperienceLetterCommand
        {
            ApplicantId = 0,
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow.AddDays(-1),
            SupervisorName = "Jane Supervisor"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId)
            .WithErrorMessage("Applicant ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_StartDate_Equals_EndDate()
    {
        var endDate = DateTime.UtcNow.AddDays(-1);
        var command = new GenerateExperienceLetterCommand
        {
            ApplicantId = 1,
            StartDate = endDate,
            EndDate = endDate,
            SupervisorName = "Jane Supervisor"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StartDate)
            .WithErrorMessage("Start date must be before end date.");
    }

    [Fact]
    public void Should_Have_Error_When_StartDate_Is_After_EndDate()
    {
        var command = new GenerateExperienceLetterCommand
        {
            ApplicantId = 1,
            StartDate = DateTime.UtcNow.AddDays(-1),
            EndDate = DateTime.UtcNow.AddDays(-30),
            SupervisorName = "Jane Supervisor"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StartDate)
            .WithErrorMessage("Start date must be before end date.");
    }

    [Fact]
    public void Should_Have_Error_When_EndDate_Is_In_The_Future()
    {
        var command = new GenerateExperienceLetterCommand
        {
            ApplicantId = 1,
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow.AddDays(30),
            SupervisorName = "Jane Supervisor"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithErrorMessage("End date cannot be in the future.");
    }

    [Fact]
    public void Should_Have_Error_When_SupervisorName_Is_Empty()
    {
        var command = new GenerateExperienceLetterCommand
        {
            ApplicantId = 1,
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow.AddDays(-1),
            SupervisorName = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.SupervisorName)
            .WithErrorMessage("Supervisor name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_SupervisorName_Exceeds_MaxLength()
    {
        var command = new GenerateExperienceLetterCommand
        {
            ApplicantId = 1,
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow.AddDays(-1),
            SupervisorName = new string('j', 201)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.SupervisorName)
            .WithErrorMessage("Supervisor name must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new GenerateExperienceLetterCommand
        {
            ApplicantId = 1,
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow.AddDays(-1),
            SupervisorName = "Jane Supervisor"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}