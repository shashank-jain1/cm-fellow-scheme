using CmScheme.WorkAllocation.Application.Features.TaskProgress.CreateTaskProgress;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.WorkAllocation;

public class CreateTaskProgressCommandValidatorTests
{
    private readonly CreateTaskProgressCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_WorkAllocationId_Is_Zero()
    {
        var command = new CreateTaskProgressCommand
        {
            WorkAllocationId = 0,
            ProjectName = "CM Scheme 2026",
            WorkProject = "Field surveys",
            WorkDescription = "Conduct household surveys in assigned blocks.",
            Priority = "High",
            NumberOfSurveys = 50,
            CompletedSurveys = 10,
            WorkStatus = "In Progress",
            CompletionPercentage = 20m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkAllocationId)
            .WithErrorMessage("WorkAllocationId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_ProjectName_Is_Empty()
    {
        var command = new CreateTaskProgressCommand
        {
            WorkAllocationId = 1,
            ProjectName = "",
            WorkProject = "Field surveys",
            WorkDescription = "Conduct household surveys in assigned blocks.",
            Priority = "High",
            NumberOfSurveys = 50,
            CompletedSurveys = 10,
            WorkStatus = "In Progress",
            CompletionPercentage = 20m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectName)
            .WithErrorMessage("ProjectName is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ProjectName_Exceeds_MaximumLength()
    {
        var command = new CreateTaskProgressCommand
        {
            WorkAllocationId = 1,
            ProjectName = new string('P', 201),
            WorkProject = "Field surveys",
            WorkDescription = "Conduct household surveys in assigned blocks.",
            Priority = "High",
            NumberOfSurveys = 50,
            CompletedSurveys = 10,
            WorkStatus = "In Progress",
            CompletionPercentage = 20m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectName)
            .WithErrorMessage("ProjectName must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_WorkProject_Is_Empty()
    {
        var command = new CreateTaskProgressCommand
        {
            WorkAllocationId = 1,
            ProjectName = "CM Scheme 2026",
            WorkProject = "",
            WorkDescription = "Conduct household surveys in assigned blocks.",
            Priority = "High",
            NumberOfSurveys = 50,
            CompletedSurveys = 10,
            WorkStatus = "In Progress",
            CompletionPercentage = 20m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkProject)
            .WithErrorMessage("WorkProject is required.");
    }

    [Fact]
    public void Should_Have_Error_When_NumberOfSurveys_Is_Zero()
    {
        var command = new CreateTaskProgressCommand
        {
            WorkAllocationId = 1,
            ProjectName = "CM Scheme 2026",
            WorkProject = "Field surveys",
            WorkDescription = "Conduct household surveys in assigned blocks.",
            Priority = "High",
            NumberOfSurveys = 0,
            CompletedSurveys = 10,
            WorkStatus = "In Progress",
            CompletionPercentage = 20m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.NumberOfSurveys)
            .WithErrorMessage("NumberOfSurveys must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_CompletedSurveys_Is_Negative()
    {
        var command = new CreateTaskProgressCommand
        {
            WorkAllocationId = 1,
            ProjectName = "CM Scheme 2026",
            WorkProject = "Field surveys",
            WorkDescription = "Conduct household surveys in assigned blocks.",
            Priority = "High",
            NumberOfSurveys = 50,
            CompletedSurveys = -1,
            WorkStatus = "In Progress",
            CompletionPercentage = 20m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CompletedSurveys)
            .WithErrorMessage("CompletedSurveys must be greater than or equal to 0.");
    }

    [Fact]
    public void Should_Have_Error_When_CompletionPercentage_Is_Out_Of_Range()
    {
        var command = new CreateTaskProgressCommand
        {
            WorkAllocationId = 1,
            ProjectName = "CM Scheme 2026",
            WorkProject = "Field surveys",
            WorkDescription = "Conduct household surveys in assigned blocks.",
            Priority = "High",
            NumberOfSurveys = 50,
            CompletedSurveys = 10,
            WorkStatus = "In Progress",
            CompletionPercentage = 101m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CompletionPercentage)
            .WithErrorMessage("CompletionPercentage must be between 0 and 100.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateTaskProgressCommand
        {
            WorkAllocationId = 1,
            ProjectName = "CM Scheme 2026",
            WorkProject = "Field surveys",
            WorkDescription = "Conduct household surveys in assigned blocks.",
            Priority = "High",
            NumberOfSurveys = 50,
            CompletedSurveys = 10,
            CompletionDate = new DateTime(2026, 9, 9),
            WorkStatus = "In Progress",
            CompletionPercentage = 20m
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}