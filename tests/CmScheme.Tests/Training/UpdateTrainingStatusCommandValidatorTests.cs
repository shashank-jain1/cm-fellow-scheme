using CmScheme.Common.Core;
using CmScheme.Training.Application.Features.Training.UpdateTrainingStatus;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Training;

public class UpdateTrainingStatusCommandValidatorTests
{
    private readonly UpdateTrainingStatusCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_TrainingScheduleId_Is_Zero()
    {
        var command = new UpdateTrainingStatusCommand
        {
            TrainingScheduleId = 0,
            NewStatus = Statuses.Training.Ongoing
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TrainingScheduleId)
            .WithErrorMessage("Valid training schedule ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_TrainingScheduleId_Is_Negative()
    {
        var command = new UpdateTrainingStatusCommand
        {
            TrainingScheduleId = -1,
            NewStatus = Statuses.Training.Ongoing
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TrainingScheduleId)
            .WithErrorMessage("Valid training schedule ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_NewStatus_Is_Empty()
    {
        var command = new UpdateTrainingStatusCommand
        {
            TrainingScheduleId = 1,
            NewStatus = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.NewStatus)
            .WithErrorMessage("New status is required.");
    }

    [Fact]
    public void Should_Have_Error_When_NewStatus_Is_Not_Allowed()
    {
        var command = new UpdateTrainingStatusCommand
        {
            TrainingScheduleId = 1,
            NewStatus = "InReview"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.NewStatus)
            .WithErrorMessage("Invalid training status.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_NewStatus_Is_Allowed_With_Different_Casing()
    {
        var command = new UpdateTrainingStatusCommand
        {
            TrainingScheduleId = 1,
            NewStatus = "ongoing"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.NewStatus);
    }

    [Fact]
    public void Should_Not_Have_Error_When_NewStatus_Is_Cancelled()
    {
        var command = new UpdateTrainingStatusCommand
        {
            TrainingScheduleId = 1,
            NewStatus = Statuses.Training.Cancelled
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.NewStatus);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdateTrainingStatusCommand
        {
            TrainingScheduleId = 1,
            NewStatus = Statuses.Training.Completed
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}