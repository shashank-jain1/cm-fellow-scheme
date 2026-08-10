using CmScheme.Masters.Application.Features.Works.UpdateWork;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class UpdateWorkCommandValidatorTests
{
    private readonly UpdateWorkCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_WorkId_Is_Zero()
    {
        var command = new UpdateWorkCommand
        {
            WorkId = 0,
            ProjectId = 1,
            WorkName = "Borewell Installation",
            Priority = "High",
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 9, 30),
            AssignedTo = "Engineer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkId)
            .WithErrorMessage("Work ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ProjectId_Is_Zero()
    {
        var command = new UpdateWorkCommand
        {
            WorkId = 1,
            ProjectId = 0,
            WorkName = "Borewell Installation",
            Priority = "High",
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 9, 30),
            AssignedTo = "Engineer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectId)
            .WithErrorMessage("Project ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_WorkName_Is_Empty()
    {
        var command = new UpdateWorkCommand
        {
            WorkId = 1,
            ProjectId = 1,
            WorkName = "",
            Priority = "High",
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 9, 30),
            AssignedTo = "Engineer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkName)
            .WithErrorMessage("Work Name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_WorkName_Exceeds_MaximumLength()
    {
        var command = new UpdateWorkCommand
        {
            WorkId = 1,
            ProjectId = 1,
            WorkName = new string('W', 251),
            Priority = "High",
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 9, 30),
            AssignedTo = "Engineer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkName)
            .WithErrorMessage("Work Name must not exceed 250 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Priority_Is_Empty()
    {
        var command = new UpdateWorkCommand
        {
            WorkId = 1,
            ProjectId = 1,
            WorkName = "Borewell Installation",
            Priority = "",
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 9, 30),
            AssignedTo = "Engineer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Priority)
            .WithErrorMessage("Priority is required.");
    }

    [Fact]
    public void Should_Have_Error_When_EndDate_Is_Before_StartDate()
    {
        var command = new UpdateWorkCommand
        {
            WorkId = 1,
            ProjectId = 1,
            WorkName = "Borewell Installation",
            Priority = "High",
            StartDate = new DateTime(2025, 9, 30),
            EndDate = new DateTime(2025, 5, 1),
            AssignedTo = "Engineer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithErrorMessage("End Date must be on or after Start Date.");
    }

    [Fact]
    public void Should_Have_Error_When_AssignedTo_Exceeds_MaximumLength()
    {
        var command = new UpdateWorkCommand
        {
            WorkId = 1,
            ProjectId = 1,
            WorkName = "Borewell Installation",
            Priority = "High",
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 9, 30),
            AssignedTo = new string('A', 151)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.AssignedTo)
            .WithErrorMessage("Assigned To must not exceed 150 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdateWorkCommand
        {
            WorkId = 1,
            ProjectId = 1,
            WorkName = "Borewell Installation",
            WorkDescription = "Install borewell in village",
            Priority = "High",
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 9, 30),
            AssignedTo = "Engineer",
            Remarks = "Follow up weekly"
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}