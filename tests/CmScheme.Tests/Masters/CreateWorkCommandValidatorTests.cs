using CmScheme.Masters.Application.Features.Works.CreateWork;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class CreateWorkCommandValidatorTests
{
    private readonly CreateWorkCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ProjectId_Is_Zero()
    {
        var command = new CreateWorkCommand
        {
            ProjectId = 0,
            WorkName = "Borewell Installation",
            Priority = "High",
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 9, 30),
            AssignedTo = "Engineer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectId)
            .WithErrorMessage("Valid project is required.");
    }

    [Fact]
    public void Should_Have_Error_When_WorkName_Is_Empty()
    {
        var command = new CreateWorkCommand
        {
            ProjectId = 1,
            WorkName = "",
            Priority = "High",
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 9, 30),
            AssignedTo = "Engineer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkName)
            .WithErrorMessage("Work name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_WorkName_Exceeds_MaximumLength()
    {
        var command = new CreateWorkCommand
        {
            ProjectId = 1,
            WorkName = new string('W', 251),
            Priority = "High",
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 9, 30),
            AssignedTo = "Engineer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkName)
            .WithErrorMessage("Work name must not exceed 250 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Priority_Is_Empty()
    {
        var command = new CreateWorkCommand
        {
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
    public void Should_Have_Error_When_Priority_Is_Not_Valid()
    {
        var command = new CreateWorkCommand
        {
            ProjectId = 1,
            WorkName = "Borewell Installation",
            Priority = "Urgent",
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 9, 30),
            AssignedTo = "Engineer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Priority)
            .WithErrorMessage("Priority must be Low, Medium, High, or Critical.");
    }

    [Fact]
    public void Should_Have_Error_When_StartDate_Is_Not_Provided()
    {
        var command = new CreateWorkCommand
        {
            ProjectId = 1,
            WorkName = "Borewell Installation",
            Priority = "High",
            StartDate = default,
            EndDate = new DateTime(2025, 9, 30),
            AssignedTo = "Engineer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StartDate)
            .WithErrorMessage("Start date is required.");
    }

    [Fact]
    public void Should_Have_Error_When_EndDate_Is_Before_StartDate()
    {
        var command = new CreateWorkCommand
        {
            ProjectId = 1,
            WorkName = "Borewell Installation",
            Priority = "High",
            StartDate = new DateTime(2025, 9, 30),
            EndDate = new DateTime(2025, 5, 1),
            AssignedTo = "Engineer"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithErrorMessage("End date must be after start date.");
    }

    [Fact]
    public void Should_Have_Error_When_AssignedTo_Is_Empty()
    {
        var command = new CreateWorkCommand
        {
            ProjectId = 1,
            WorkName = "Borewell Installation",
            Priority = "High",
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 9, 30),
            AssignedTo = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.AssignedTo)
            .WithErrorMessage("Assigned to is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateWorkCommand
        {
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