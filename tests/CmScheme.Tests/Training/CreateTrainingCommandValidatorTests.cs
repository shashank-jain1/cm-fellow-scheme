using CmScheme.Training.Application.Features.Training.CreateTraining;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Training;

public class CreateTrainingCommandValidatorTests
{
    private readonly IValidator<CreateTrainingCommand> _validator = CreateValidator();

    private static IValidator<CreateTrainingCommand> CreateValidator()
    {
        var type = typeof(CreateTrainingCommand).Assembly.GetType(
            "CmScheme.Training.Application.Features.Training.CreateTraining.CreateTrainingCommandValidator",
            throwOnError: true)!;
        return (IValidator<CreateTrainingCommand>)Activator.CreateInstance(type)!;
    }

    [Fact]
    public void Should_Have_Error_When_ProjectId_Is_Zero()
    {
        var command = new CreateTrainingCommand
        {
            ProjectId = 0,
            TrainingTitle = "Fellow induction",
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 9, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 17, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectId)
            .WithErrorMessage("Valid project is required.");
    }

    [Fact]
    public void Should_Have_Error_When_TrainingTitle_Is_Empty()
    {
        var command = new CreateTrainingCommand
        {
            ProjectId = 1,
            TrainingTitle = "",
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 9, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 17, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TrainingTitle)
            .WithErrorMessage("Training title is required.");
    }

    [Fact]
    public void Should_Have_Error_When_TrainingTitle_Exceeds_MaximumLength()
    {
        var command = new CreateTrainingCommand
        {
            ProjectId = 1,
            TrainingTitle = new string('T', 251),
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 9, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 17, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TrainingTitle)
            .WithErrorMessage("Training title must not exceed 250 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Date_Is_Empty()
    {
        var command = new CreateTrainingCommand
        {
            ProjectId = 1,
            TrainingTitle = "Fellow induction",
            Date = default,
            StartTime = new DateTime(2026, 8, 10, 9, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 17, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Date)
            .WithErrorMessage("Training date is required.");
    }

    [Fact]
    public void Should_Have_Error_When_EndTime_Is_Before_StartTime()
    {
        var command = new CreateTrainingCommand
        {
            ProjectId = 1,
            TrainingTitle = "Fellow induction",
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 17, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 9, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndTime)
            .WithErrorMessage("End time must be after start time.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateTrainingCommand
        {
            ProjectId = 1,
            WorkProjectId = 1,
            TrainingTitle = "Fellow induction",
            TrainingCategory = "Onboarding",
            TrainingDescription = "Induction of new fellows.",
            TargetUserTypes = new List<string> { "Fellow" },
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 9, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 17, 0, 0),
            Mode = "Offline",
            TrainerName = "Trainer",
            TrainerMobile = "9876543210",
            AttendanceRequired = true,
            CertificateRequired = true,
            Remarks = "Please carry ID cards."
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}