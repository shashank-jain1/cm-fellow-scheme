using CmScheme.Masters.Application.Features.Projects.UpdateProject;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class UpdateProjectCommandValidatorTests
{
    private readonly UpdateProjectCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ProjectId_Is_Zero()
    {
        var command = new UpdateProjectCommand
        {
            ProjectId = 0,
            ProjectName = "Rural Roads",
            ProjectCode = "PRJ001",
            DepartmentName = "Panchayat",
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = "Collector"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectId)
            .WithErrorMessage("ProjectId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_ProjectName_Is_Empty()
    {
        var command = new UpdateProjectCommand
        {
            ProjectId = 1,
            ProjectName = "",
            ProjectCode = "PRJ001",
            DepartmentName = "Panchayat",
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = "Collector"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectName)
            .WithErrorMessage("ProjectName is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ProjectCode_Exceeds_MaximumLength()
    {
        var command = new UpdateProjectCommand
        {
            ProjectId = 1,
            ProjectName = "Rural Roads",
            ProjectCode = new string('C', 21),
            DepartmentName = "Panchayat",
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = "Collector"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectCode)
            .WithErrorMessage("ProjectCode must not exceed 20 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_DepartmentName_Exceeds_MaximumLength()
    {
        var command = new UpdateProjectCommand
        {
            ProjectId = 1,
            ProjectName = "Rural Roads",
            ProjectCode = "PRJ001",
            DepartmentName = new string('D', 151),
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = "Collector"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DepartmentName)
            .WithErrorMessage("DepartmentName must not exceed 150 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_StartDate_Is_Not_Provided()
    {
        var command = new UpdateProjectCommand
        {
            ProjectId = 1,
            ProjectName = "Rural Roads",
            ProjectCode = "PRJ001",
            DepartmentName = "Panchayat",
            StartDate = default,
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = "Collector"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StartDate)
            .WithErrorMessage("StartDate is required.");
    }

    [Fact]
    public void Should_Have_Error_When_EndDate_Is_Not_After_StartDate()
    {
        var startDate = new DateTime(2025, 4, 1);
        var command = new UpdateProjectCommand
        {
            ProjectId = 1,
            ProjectName = "Rural Roads",
            ProjectCode = "PRJ001",
            DepartmentName = "Panchayat",
            StartDate = startDate,
            EndDate = startDate,
            ProjectIncharge = "Collector"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithErrorMessage("EndDate must be after StartDate.");
    }

    [Fact]
    public void Should_Have_Error_When_ProjectIncharge_Exceeds_MaximumLength()
    {
        var command = new UpdateProjectCommand
        {
            ProjectId = 1,
            ProjectName = "Rural Roads",
            ProjectCode = "PRJ001",
            DepartmentName = "Panchayat",
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = new string('I', 151)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectIncharge)
            .WithErrorMessage("ProjectIncharge must not exceed 150 characters.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdateProjectCommand
        {
            ProjectId = 1,
            ProjectName = "Rural Roads",
            ProjectCode = "PRJ001",
            ProjectDescription = "Improvement of rural roads",
            DepartmentName = "Panchayat",
            StartDate = new DateTime(2025, 4, 1),
            EndDate = new DateTime(2025, 12, 31),
            ProjectIncharge = "Collector",
            BudgetAmount = 5000000m,
            BudgetApprovedBy = 1,
            IsActive = true
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}