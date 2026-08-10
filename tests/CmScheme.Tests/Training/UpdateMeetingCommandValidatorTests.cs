using CmScheme.Training.Application.Features.Meeting.UpdateMeeting;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Training;

public class UpdateMeetingCommandValidatorTests
{
    private readonly UpdateMeetingCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_TrainingScheduleId_Is_Zero()
    {
        var command = new UpdateMeetingCommand
        {
            TrainingScheduleId = 0,
            MeetingTitle = "Monthly review",
            MeetingAgenda = "Progress update",
            MeetingDescription = "Discussion on field work.",
            ConductPersonId = 1,
            CoordinatorId = 1,
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 12, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TrainingScheduleId)
            .WithErrorMessage("Meeting ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_MeetingTitle_Is_Empty()
    {
        var command = new UpdateMeetingCommand
        {
            TrainingScheduleId = 1,
            MeetingTitle = "",
            MeetingAgenda = "Progress update",
            MeetingDescription = "Discussion on field work.",
            ConductPersonId = 1,
            CoordinatorId = 1,
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 12, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MeetingTitle)
            .WithErrorMessage("Meeting Title is required.");
    }

    [Fact]
    public void Should_Have_Error_When_MeetingTitle_Exceeds_MaximumLength()
    {
        var command = new UpdateMeetingCommand
        {
            TrainingScheduleId = 1,
            MeetingTitle = new string('M', 251),
            MeetingAgenda = "Progress update",
            MeetingDescription = "Discussion on field work.",
            ConductPersonId = 1,
            CoordinatorId = 1,
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 12, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MeetingTitle)
            .WithErrorMessage("Meeting Title must not exceed 250 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_MeetingAgenda_Is_Empty()
    {
        var command = new UpdateMeetingCommand
        {
            TrainingScheduleId = 1,
            MeetingTitle = "Monthly review",
            MeetingAgenda = "",
            MeetingDescription = "Discussion on field work.",
            ConductPersonId = 1,
            CoordinatorId = 1,
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 12, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MeetingAgenda)
            .WithErrorMessage("Meeting Agenda is required.");
    }

    [Fact]
    public void Should_Have_Error_When_MeetingDescription_Is_Empty()
    {
        var command = new UpdateMeetingCommand
        {
            TrainingScheduleId = 1,
            MeetingTitle = "Monthly review",
            MeetingAgenda = "Progress update",
            MeetingDescription = "",
            ConductPersonId = 1,
            CoordinatorId = 1,
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 12, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MeetingDescription)
            .WithErrorMessage("Meeting Description is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ConductPersonId_Is_Zero()
    {
        var command = new UpdateMeetingCommand
        {
            TrainingScheduleId = 1,
            MeetingTitle = "Monthly review",
            MeetingAgenda = "Progress update",
            MeetingDescription = "Discussion on field work.",
            ConductPersonId = 0,
            CoordinatorId = 1,
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 12, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ConductPersonId)
            .WithErrorMessage("Conduct Person is required.");
    }

    [Fact]
    public void Should_Have_Error_When_CoordinatorId_Is_Zero()
    {
        var command = new UpdateMeetingCommand
        {
            TrainingScheduleId = 1,
            MeetingTitle = "Monthly review",
            MeetingAgenda = "Progress update",
            MeetingDescription = "Discussion on field work.",
            ConductPersonId = 1,
            CoordinatorId = 0,
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 12, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CoordinatorId)
            .WithErrorMessage("Coordinator is required.");
    }

    [Fact]
    public void Should_Have_Error_When_EndTime_Is_Before_StartTime()
    {
        var command = new UpdateMeetingCommand
        {
            TrainingScheduleId = 1,
            MeetingTitle = "Monthly review",
            MeetingAgenda = "Progress update",
            MeetingDescription = "Discussion on field work.",
            ConductPersonId = 1,
            CoordinatorId = 1,
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 12, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 10, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndTime)
            .WithErrorMessage("End Time must be greater than Start Time.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdateMeetingCommand
        {
            TrainingScheduleId = 1,
            MeetingTitle = "Monthly review",
            MeetingAgenda = "Progress update",
            MeetingDescription = "Discussion on field work.",
            ConductPersonId = 1,
            CoordinatorId = 1,
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 12, 0, 0),
            MOMRequired = true,
            Remarks = "Minutes required."
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}