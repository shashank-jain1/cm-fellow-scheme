using CmScheme.WorkAllocation.Application.Features.TaskProgress.RecordSurveySubmission;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.WorkAllocation;

public class RecordSurveySubmissionCommandValidatorTests
{
    private readonly RecordSurveySubmissionCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_TaskProgressId_Is_Zero()
    {
        var command = new RecordSurveySubmissionCommand
        {
            TaskProgressId = 0,
            ApplicantId = 1,
            SurveyPersonName = "Rajesh Kumar",
            MobileNumber = "9876543210",
            PanchayatName = "Gudgaon",
            VillageName = "Deori",
            Latitude = 23.25m,
            Longitude = 77.45m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TaskProgressId)
            .WithErrorMessage("Task Progress ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ApplicantId_Is_Zero()
    {
        var command = new RecordSurveySubmissionCommand
        {
            TaskProgressId = 1,
            ApplicantId = 0,
            SurveyPersonName = "Rajesh Kumar",
            MobileNumber = "9876543210",
            PanchayatName = "Gudgaon",
            VillageName = "Deori",
            Latitude = 23.25m,
            Longitude = 77.45m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId)
            .WithErrorMessage("Applicant ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_SurveyPersonName_Is_Empty()
    {
        var command = new RecordSurveySubmissionCommand
        {
            TaskProgressId = 1,
            ApplicantId = 1,
            SurveyPersonName = "",
            MobileNumber = "9876543210",
            PanchayatName = "Gudgaon",
            VillageName = "Deori",
            Latitude = 23.25m,
            Longitude = 77.45m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.SurveyPersonName)
            .WithErrorMessage("Survey Person Name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_SurveyPersonName_Exceeds_MaximumLength()
    {
        var command = new RecordSurveySubmissionCommand
        {
            TaskProgressId = 1,
            ApplicantId = 1,
            SurveyPersonName = new string('S', 201),
            MobileNumber = "9876543210",
            PanchayatName = "Gudgaon",
            VillageName = "Deori",
            Latitude = 23.25m,
            Longitude = 77.45m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.SurveyPersonName)
            .WithErrorMessage("Survey Person Name must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_MobileNumber_Is_Not_Ten_Digits()
    {
        var command = new RecordSurveySubmissionCommand
        {
            TaskProgressId = 1,
            ApplicantId = 1,
            SurveyPersonName = "Rajesh Kumar",
            MobileNumber = "987654321",
            PanchayatName = "Gudgaon",
            VillageName = "Deori",
            Latitude = 23.25m,
            Longitude = 77.45m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MobileNumber)
            .WithErrorMessage("Mobile Number must be 10 digits.");
    }

    [Fact]
    public void Should_Have_Error_When_PanchayatName_Is_Empty()
    {
        var command = new RecordSurveySubmissionCommand
        {
            TaskProgressId = 1,
            ApplicantId = 1,
            SurveyPersonName = "Rajesh Kumar",
            MobileNumber = "9876543210",
            PanchayatName = "",
            VillageName = "Deori",
            Latitude = 23.25m,
            Longitude = 77.45m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PanchayatName)
            .WithErrorMessage("Panchayat Name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Latitude_Is_Out_Of_Range()
    {
        var command = new RecordSurveySubmissionCommand
        {
            TaskProgressId = 1,
            ApplicantId = 1,
            SurveyPersonName = "Rajesh Kumar",
            MobileNumber = "9876543210",
            PanchayatName = "Gudgaon",
            VillageName = "Deori",
            Latitude = 91m,
            Longitude = 77.45m
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Latitude)
            .WithErrorMessage("Latitude must be between -90 and 90.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new RecordSurveySubmissionCommand
        {
            TaskProgressId = 1,
            ApplicantId = 1,
            SurveyPersonName = "Rajesh Kumar",
            MobileNumber = "9876543210",
            PanchayatName = "Gudgaon",
            VillageName = "Deori",
            Latitude = 23.25m,
            Longitude = 77.45m
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}