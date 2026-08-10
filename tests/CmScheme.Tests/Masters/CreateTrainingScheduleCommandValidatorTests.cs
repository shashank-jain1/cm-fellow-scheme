using CmScheme.Masters.Application.Features.TrainingSchedules.CreateTrainingSchedule;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class CreateTrainingScheduleCommandValidatorTests
{
    private readonly CreateTrainingScheduleCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_CalendarYear_Is_Empty()
    {
        var command = new CreateTrainingScheduleCommand
        {
            CalendarYear = "",
            ProjectId = 1,
            TrainingDate = new DateTime(2025, 6, 15)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CalendarYear)
            .WithErrorMessage("Calendar Year is required.");
    }

    [Fact]
    public void Should_Have_Error_When_CalendarYear_Exceeds_MaximumLength()
    {
        var command = new CreateTrainingScheduleCommand
        {
            CalendarYear = new string('Y', 21),
            ProjectId = 1,
            TrainingDate = new DateTime(2025, 6, 15)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CalendarYear)
            .WithErrorMessage("Calendar Year must not exceed 20 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_ProjectId_Is_Zero()
    {
        var command = new CreateTrainingScheduleCommand
        {
            CalendarYear = "2025-26",
            ProjectId = 0,
            TrainingDate = new DateTime(2025, 6, 15)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectId)
            .WithErrorMessage("Project is required.");
    }

    [Fact]
    public void Should_Have_Error_When_TrainingDate_Is_Not_Provided()
    {
        var command = new CreateTrainingScheduleCommand
        {
            CalendarYear = "2025-26",
            ProjectId = 1,
            TrainingDate = default
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TrainingDate)
            .WithErrorMessage("Training Date is required.");
    }

    [Fact]
    public void Should_Have_Error_When_VenueName_Exceeds_MaximumLength()
    {
        var command = new CreateTrainingScheduleCommand
        {
            CalendarYear = "2025-26",
            ProjectId = 1,
            TrainingDate = new DateTime(2025, 6, 15),
            VenueName = new string('V', 251)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.VenueName)
            .WithErrorMessage("Venue Name must not exceed 250 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_TrainingDescription_Exceeds_MaximumLength()
    {
        var command = new CreateTrainingScheduleCommand
        {
            CalendarYear = "2025-26",
            ProjectId = 1,
            TrainingDate = new DateTime(2025, 6, 15),
            TrainingDescription = new string('T', 1001)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TrainingDescription)
            .WithErrorMessage("Training Description must not exceed 1000 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateTrainingScheduleCommand
        {
            CalendarYear = "2025-26",
            ProjectId = 1,
            WorkId = 1,
            DivisionId = 1,
            DistrictId = 1,
            BlockId = 1,
            TrainingDate = new DateTime(2025, 6, 15),
            VenueName = "Gram Panchayat Bhawan",
            TrainingDescription = "Monthly training session"
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}