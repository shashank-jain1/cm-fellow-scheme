using CmScheme.Training.Application.Features.Meeting.CreateMeeting;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Training;

public class CreateMeetingCommandValidatorTests
{
    private readonly IValidator<CreateMeetingCommand> _validator = CreateValidator();

    private static IValidator<CreateMeetingCommand> CreateValidator()
    {
        var type = typeof(CreateMeetingCommand).Assembly.GetType(
            "CmScheme.Training.Application.Features.Meeting.CreateMeeting.CreateMeetingCommandValidator",
            throwOnError: true)!;
        return (IValidator<CreateMeetingCommand>)Activator.CreateInstance(type)!;
    }

    [Fact]
    public void Should_Have_Error_When_MeetingTitle_Is_Empty()
    {
        var command = new CreateMeetingCommand
        {
            MeetingTitle = "",
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 12, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MeetingTitle)
            .WithErrorMessage("Meeting title is required.");
    }

    [Fact]
    public void Should_Have_Error_When_MeetingTitle_Exceeds_MaximumLength()
    {
        var command = new CreateMeetingCommand
        {
            MeetingTitle = new string('M', 251),
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 12, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MeetingTitle)
            .WithErrorMessage("Meeting title must not exceed 250 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Date_Is_Empty()
    {
        var command = new CreateMeetingCommand
        {
            MeetingTitle = "Monthly review",
            Date = default,
            StartTime = new DateTime(2026, 8, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 12, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Date)
            .WithErrorMessage("Meeting date is required.");
    }

    [Fact]
    public void Should_Have_Error_When_EndTime_Is_Before_StartTime()
    {
        var command = new CreateMeetingCommand
        {
            MeetingTitle = "Monthly review",
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 12, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 10, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndTime)
            .WithErrorMessage("End time must be after start time.");
    }

    [Fact]
    public void Should_Have_Error_When_EndTime_Equals_StartTime()
    {
        var command = new CreateMeetingCommand
        {
            MeetingTitle = "Monthly review",
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 10, 0, 0)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndTime)
            .WithErrorMessage("End time must be after start time.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateMeetingCommand
        {
            ProjectId = 1,
            WorkProjectId = 1,
            MeetingTitle = "Monthly review",
            MeetingAgenda = "Progress update",
            MeetingDescription = "Discussion on field work.",
            ConductPersonId = 1,
            CoordinatorId = 1,
            ParticipantIds = new List<int> { 1, 2 },
            Date = new DateTime(2026, 8, 10),
            StartTime = new DateTime(2026, 8, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 8, 10, 12, 0, 0),
            Mode = "Offline",
            MOMRequired = true,
            Remarks = "Minutes required."
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}