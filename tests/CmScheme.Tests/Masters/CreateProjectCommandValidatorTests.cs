using CmScheme.Masters.Application.Features.Projects.CreateProject;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class CreateProjectCommandValidatorTests
{
    private readonly CreateProjectCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ProjectName_Is_Empty()
    {
        var command = new CreateProjectCommand
        {
            ProjectName = "",
            ProjectCode = "PRJ001",
            DepartmentName = "Panchayat",
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = "Collector"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectName)
            .WithErrorMessage("Project name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ProjectName_Exceeds_MaximumLength()
    {
        var command = new CreateProjectCommand
        {
            ProjectName = new string('P', 251),
            ProjectCode = "PRJ001",
            DepartmentName = "Panchayat",
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = "Collector"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectName)
            .WithErrorMessage("Project name must not exceed 250 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_ProjectCode_Is_Empty()
    {
        var command = new CreateProjectCommand
        {
            ProjectName = "Rural Roads",
            ProjectCode = "",
            DepartmentName = "Panchayat",
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = "Collector"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectCode)
            .WithErrorMessage("Project code is required.");
    }

    [Fact]
    public void Should_Have_Error_When_DepartmentName_Is_Empty()
    {
        var command = new CreateProjectCommand
        {
            ProjectName = "Rural Roads",
            ProjectCode = "PRJ001",
            DepartmentName = "",
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = "Collector"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DepartmentName)
            .WithErrorMessage("Department name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_StartDate_Is_Not_Provided()
    {
        var command = new CreateProjectCommand
        {
            ProjectName = "Rural Roads",
            ProjectCode = "PRJ001",
            DepartmentName = "Panchayat",
            StartDate = default,
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = "Collector"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StartDate)
            .WithErrorMessage("Start date is required.");
    }

    [Fact]
    public void Should_Have_Error_When_EndDate_Is_Before_StartDate()
    {
        var command = new CreateProjectCommand
        {
            ProjectName = "Rural Roads",
            ProjectCode = "PRJ001",
            DepartmentName = "Panchayat",
            StartDate = new DateTime(2025, 12, 31),
            EndDate = new DateTime(2025, 4, 1),
            ProjectIncharge = "Collector"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithErrorMessage("End date must be after start date.");
    }

    [Fact]
    public void Should_Have_Error_When_ProjectIncharge_Is_Empty()
    {
        var command = new CreateProjectCommand
        {
            ProjectName = "Rural Roads",
            ProjectCode = "PRJ001",
            DepartmentName = "Panchayat",
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectIncharge)
            .WithErrorMessage("Project incharge is required.");
    }

    [Fact]
    public void Should_Have_Error_When_BudgetAmount_Is_Negative()
    {
        var command = new CreateProjectCommand
        {
            ProjectName = "Rural Roads",
            ProjectCode = "PRJ001",
            DepartmentName = "Panchayat",
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = "Collector",
            BudgetAmount = -1000m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.BudgetAmount)
            .WithErrorMessage("Budget amount must be non-negative.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateProjectCommand
        {
            ProjectName = "Rural Roads",
            ProjectCode = "PRJ001",
            ProjectDescription = "Improvement of rural roads",
            DepartmentName = "Panchayat",
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = "Collector",
            BudgetAmount = 5000000m,
            BudgetApprovedBy = 1,
            ProjectDocumentPath = "/docs/prj001.pdf"
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}