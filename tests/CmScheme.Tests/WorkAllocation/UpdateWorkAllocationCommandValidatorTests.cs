using CmScheme.WorkAllocation.Application.Features.WorkAllocation.UpdateWorkAllocation;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.WorkAllocation;

public class UpdateWorkAllocationCommandValidatorTests
{
    private readonly UpdateWorkAllocationCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_WorkAllocationId_Is_Zero()
    {
        var command = new UpdateWorkAllocationCommand
        {
            WorkAllocationId = 0,
            ProjectId = 1,
            WorkProjectId = "WP-2026-001",
            WorkDescription = "Field survey of 500 households.",
            Priority = "High",
            DurationDays = 30,
            SurveysPerIntern = 25,
            Status = "Assigned",
            ModifiedBy = "Coordinator"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkAllocationId)
            .WithErrorMessage("WorkAllocationId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_ProjectId_Is_Zero()
    {
        var command = new UpdateWorkAllocationCommand
        {
            WorkAllocationId = 1,
            ProjectId = 0,
            WorkProjectId = "WP-2026-001",
            WorkDescription = "Field survey of 500 households.",
            Priority = "High",
            DurationDays = 30,
            SurveysPerIntern = 25,
            Status = "Assigned",
            ModifiedBy = "Coordinator"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectId)
            .WithErrorMessage("ProjectId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_WorkProjectId_Is_Empty()
    {
        var command = new UpdateWorkAllocationCommand
        {
            WorkAllocationId = 1,
            ProjectId = 1,
            WorkProjectId = "",
            WorkDescription = "Field survey of 500 households.",
            Priority = "High",
            DurationDays = 30,
            SurveysPerIntern = 25,
            Status = "Assigned",
            ModifiedBy = "Coordinator"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkProjectId)
            .WithErrorMessage("WorkProjectId is required.");
    }

    [Fact]
    public void Should_Have_Error_When_WorkProjectId_Exceeds_MaximumLength()
    {
        var command = new UpdateWorkAllocationCommand
        {
            WorkAllocationId = 1,
            ProjectId = 1,
            WorkProjectId = new string('W', 51),
            WorkDescription = "Field survey of 500 households.",
            Priority = "High",
            DurationDays = 30,
            SurveysPerIntern = 25,
            Status = "Assigned",
            ModifiedBy = "Coordinator"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkProjectId)
            .WithErrorMessage("WorkProjectId must not exceed 50 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_WorkDescription_Exceeds_MaximumLength()
    {
        var command = new UpdateWorkAllocationCommand
        {
            WorkAllocationId = 1,
            ProjectId = 1,
            WorkProjectId = "WP-2026-001",
            WorkDescription = new string('D', 2001),
            Priority = "High",
            DurationDays = 30,
            SurveysPerIntern = 25,
            Status = "Assigned",
            ModifiedBy = "Coordinator"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkDescription)
            .WithErrorMessage("WorkDescription must not exceed 2000 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_DurationDays_Is_Zero()
    {
        var command = new UpdateWorkAllocationCommand
        {
            WorkAllocationId = 1,
            ProjectId = 1,
            WorkProjectId = "WP-2026-001",
            WorkDescription = "Field survey of 500 households.",
            Priority = "High",
            DurationDays = 0,
            SurveysPerIntern = 25,
            Status = "Assigned",
            ModifiedBy = "Coordinator"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DurationDays)
            .WithErrorMessage("DurationDays must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_ModifiedBy_Is_Empty()
    {
        var command = new UpdateWorkAllocationCommand
        {
            WorkAllocationId = 1,
            ProjectId = 1,
            WorkProjectId = "WP-2026-001",
            WorkDescription = "Field survey of 500 households.",
            Priority = "High",
            DurationDays = 30,
            SurveysPerIntern = 25,
            Status = "Assigned",
            ModifiedBy = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ModifiedBy)
            .WithErrorMessage("ModifiedBy is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdateWorkAllocationCommand
        {
            WorkAllocationId = 1,
            ProjectId = 1,
            WorkProjectId = "WP-2026-001",
            WorkDescription = "Field survey of 500 households.",
            Priority = "High",
            StartDate = new DateTime(2026, 8, 10),
            EndDate = new DateTime(2026, 9, 9),
            DurationDays = 30,
            SurveysPerIntern = 25,
            DivisionId = 1,
            DistrictId = 1,
            BlockId = 1,
            ActiveStatus = true,
            Status = "Assigned",
            ModifiedBy = "Coordinator"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}