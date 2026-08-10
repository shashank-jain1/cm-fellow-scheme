using CmScheme.Masters.Application.Features.TrainingSchedules.UpdateTrainingSchedule;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class UpdateTrainingScheduleCommandValidatorTests
{
    private readonly UpdateTrainingScheduleCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_TrainingScheduleId_Is_Zero()
    {
        var command = new UpdateTrainingScheduleCommand
        {
            TrainingScheduleId = 0,
            CalendarYear = "2025-26",
            ProjectId = 1,
            TrainingDate = new DateTime(2025, 6, 15)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TrainingScheduleId)
            .WithErrorMessage("Training Schedule ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_CalendarYear_Is_Empty()
    {
        var command = new UpdateTrainingScheduleCommand
        {
            TrainingScheduleId = 1,
            CalendarYear = "",
            ProjectId = 1,
            TrainingDate = new DateTime(2025, 6, 15)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CalendarYear)
            .WithErrorMessage("Calendar Year is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ProjectId_Is_Zero()
    {
        var command = new UpdateTrainingScheduleCommand
        {
            TrainingScheduleId = 1,
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
        var command = new UpdateTrainingScheduleCommand
        {
            TrainingScheduleId = 1,
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
        var command = new UpdateTrainingScheduleCommand
        {
            TrainingScheduleId = 1,
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
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdateTrainingScheduleCommand
        {
            TrainingScheduleId = 1,
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