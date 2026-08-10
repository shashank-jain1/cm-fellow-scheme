using CmScheme.WorkAllocation.Application.Features.TaskProgress.UpdateTaskProgress;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.WorkAllocation;

public class UpdateTaskProgressCommandValidatorTests
{
    private readonly UpdateTaskProgressCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_WorkAllocationId_Is_Zero()
    {
        var command = new UpdateTaskProgressCommand
        {
            WorkAllocationId = 0,
            ProgressNotes = "Surveying nearing completion.",
            ProgressPercentage = 60,
            Status = "InProgress"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkAllocationId)
            .WithErrorMessage("WorkAllocationId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_ProgressNotes_Is_Empty()
    {
        var command = new UpdateTaskProgressCommand
        {
            WorkAllocationId = 1,
            ProgressNotes = "",
            ProgressPercentage = 60,
            Status = "InProgress"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProgressNotes)
            .WithErrorMessage("ProgressNotes is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ProgressNotes_Exceeds_MaximumLength()
    {
        var command = new UpdateTaskProgressCommand
        {
            WorkAllocationId = 1,
            ProgressNotes = new string('N', 501),
            ProgressPercentage = 60,
            Status = "InProgress"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProgressNotes)
            .WithErrorMessage("ProgressNotes must not exceed 500 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_ProgressPercentage_Is_Out_Of_Range()
    {
        var command = new UpdateTaskProgressCommand
        {
            WorkAllocationId = 1,
            ProgressNotes = "Surveying nearing completion.",
            ProgressPercentage = 150,
            Status = "InProgress"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProgressPercentage)
            .WithErrorMessage("ProgressPercentage must be between 0 and 100.");
    }

    [Fact]
    public void Should_Have_Error_When_Status_Is_Empty()
    {
        var command = new UpdateTaskProgressCommand
        {
            WorkAllocationId = 1,
            ProgressNotes = "Surveying nearing completion.",
            ProgressPercentage = 60,
            Status = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("Status must be InProgress, Completed, or Blocked.");
    }

    [Fact]
    public void Should_Have_Error_When_Status_Is_Not_Allowed()
    {
        var command = new UpdateTaskProgressCommand
        {
            WorkAllocationId = 1,
            ProgressNotes = "Surveying nearing completion.",
            ProgressPercentage = 60,
            Status = "Pending"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("Status must be InProgress, Completed, or Blocked.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdateTaskProgressCommand
        {
            WorkAllocationId = 1,
            ProgressNotes = "Surveying nearing completion.",
            ProgressPercentage = 60,
            Status = "InProgress"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}