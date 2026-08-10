using CmScheme.WorkAllocation.Application.Features.SurveyRecord.CreateSurveyRecord;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.WorkAllocation;

public class CreateSurveyRecordCommandValidatorTests
{
    private readonly CreateSurveyRecordCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_TaskProgressId_Is_Zero()
    {
        var command = new CreateSurveyRecordCommand
        {
            TaskProgressId = 0,
            InternName = "Fellow One",
            SurveyPersonName = "Rajesh Kumar",
            MobileNumber = "9876543210",
            PanchayatName = "Gudgaon",
            VillageName = "Deori",
            SurveyStatus = "Submitted"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TaskProgressId)
            .WithErrorMessage("TaskProgressId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_InternName_Is_Empty()
    {
        var command = new CreateSurveyRecordCommand
        {
            TaskProgressId = 1,
            InternName = "",
            SurveyPersonName = "Rajesh Kumar",
            MobileNumber = "9876543210",
            PanchayatName = "Gudgaon",
            VillageName = "Deori",
            SurveyStatus = "Submitted"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.InternName)
            .WithErrorMessage("InternName is required.");
    }

    [Fact]
    public void Should_Have_Error_When_SurveyPersonName_Is_Empty()
    {
        var command = new CreateSurveyRecordCommand
        {
            TaskProgressId = 1,
            InternName = "Fellow One",
            SurveyPersonName = "",
            MobileNumber = "9876543210",
            PanchayatName = "Gudgaon",
            VillageName = "Deori",
            SurveyStatus = "Submitted"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.SurveyPersonName)
            .WithErrorMessage("SurveyPersonName is required.");
    }

    [Fact]
    public void Should_Have_Error_When_SurveyPersonName_Exceeds_MaximumLength()
    {
        var command = new CreateSurveyRecordCommand
        {
            TaskProgressId = 1,
            InternName = "Fellow One",
            SurveyPersonName = new string('S', 201),
            MobileNumber = "9876543210",
            PanchayatName = "Gudgaon",
            VillageName = "Deori",
            SurveyStatus = "Submitted"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.SurveyPersonName)
            .WithErrorMessage("SurveyPersonName must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_MobileNumber_Is_Empty()
    {
        var command = new CreateSurveyRecordCommand
        {
            TaskProgressId = 1,
            InternName = "Fellow One",
            SurveyPersonName = "Rajesh Kumar",
            MobileNumber = "",
            PanchayatName = "Gudgaon",
            VillageName = "Deori",
            SurveyStatus = "Submitted"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MobileNumber)
            .WithErrorMessage("MobileNumber is required.");
    }

    [Fact]
    public void Should_Have_Error_When_PanchayatName_Is_Empty()
    {
        var command = new CreateSurveyRecordCommand
        {
            TaskProgressId = 1,
            InternName = "Fellow One",
            SurveyPersonName = "Rajesh Kumar",
            MobileNumber = "9876543210",
            PanchayatName = "",
            VillageName = "Deori",
            SurveyStatus = "Submitted"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PanchayatName)
            .WithErrorMessage("PanchayatName is required.");
    }

    [Fact]
    public void Should_Have_Error_When_VillageName_Is_Empty()
    {
        var command = new CreateSurveyRecordCommand
        {
            TaskProgressId = 1,
            InternName = "Fellow One",
            SurveyPersonName = "Rajesh Kumar",
            MobileNumber = "9876543210",
            PanchayatName = "Gudgaon",
            VillageName = "",
            SurveyStatus = "Submitted"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.VillageName)
            .WithErrorMessage("VillageName is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateSurveyRecordCommand
        {
            TaskProgressId = 1,
            InternName = "Fellow One",
            SurveyPersonName = "Rajesh Kumar",
            MobileNumber = "9876543210",
            PanchayatName = "Gudgaon",
            VillageName = "Deori",
            SurveyDate = new DateTime(2026, 8, 10),
            SurveyStatus = "Submitted",
            Latitude = 23.25m,
            Longitude = 77.45m
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}