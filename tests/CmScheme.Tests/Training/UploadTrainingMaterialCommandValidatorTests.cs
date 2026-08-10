using CmScheme.Training.Application.Features.Training.TrainingMaterial.UploadTrainingMaterial;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Training;

public class UploadTrainingMaterialCommandValidatorTests
{
    private readonly UploadTrainingMaterialCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_TrainingScheduleId_Is_Zero()
    {
        var command = new UploadTrainingMaterialCommand
        {
            TrainingScheduleId = 0,
            MaterialName = "Training Manual",
            FileName = "manual.pdf",
            FileStream = new MemoryStream(new byte[] { 1, 2, 3 }),
            ContentType = "application/pdf",
            FileSize = 1024
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TrainingScheduleId)
            .WithErrorMessage("TrainingScheduleId is required.");
    }

    [Fact]
    public void Should_Have_Error_When_TrainingScheduleId_Is_Negative()
    {
        var command = new UploadTrainingMaterialCommand
        {
            TrainingScheduleId = -1,
            MaterialName = "Training Manual",
            FileName = "manual.pdf",
            FileStream = new MemoryStream(new byte[] { 1, 2, 3 }),
            ContentType = "application/pdf",
            FileSize = 1024
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TrainingScheduleId)
            .WithErrorMessage("TrainingScheduleId is required.");
    }

    [Fact]
    public void Should_Have_Error_When_MaterialName_Is_Empty()
    {
        var command = new UploadTrainingMaterialCommand
        {
            TrainingScheduleId = 1,
            MaterialName = "",
            FileName = "manual.pdf",
            FileStream = new MemoryStream(new byte[] { 1, 2, 3 }),
            ContentType = "application/pdf",
            FileSize = 1024
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MaterialName)
            .WithErrorMessage("MaterialName is required.");
    }

    [Fact]
    public void Should_Have_Error_When_MaterialName_Exceeds_MaximumLength()
    {
        var command = new UploadTrainingMaterialCommand
        {
            TrainingScheduleId = 1,
            MaterialName = new string('M', 201),
            FileName = "manual.pdf",
            FileStream = new MemoryStream(new byte[] { 1, 2, 3 }),
            ContentType = "application/pdf",
            FileSize = 1024
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MaterialName)
            .WithErrorMessage("MaterialName must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_FileName_Is_Empty()
    {
        var command = new UploadTrainingMaterialCommand
        {
            TrainingScheduleId = 1,
            MaterialName = "Training Manual",
            FileName = "",
            FileStream = new MemoryStream(new byte[] { 1, 2, 3 }),
            ContentType = "application/pdf",
            FileSize = 1024
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.FileName)
            .WithErrorMessage("FileName is required.");
    }

    [Fact]
    public void Should_Have_Error_When_FileSize_Is_Zero()
    {
        var command = new UploadTrainingMaterialCommand
        {
            TrainingScheduleId = 1,
            MaterialName = "Training Manual",
            FileName = "manual.pdf",
            FileStream = new MemoryStream(new byte[] { 1, 2, 3 }),
            ContentType = "application/pdf",
            FileSize = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.FileSize)
            .WithErrorMessage("FileSize must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_FileSize_Is_Negative()
    {
        var command = new UploadTrainingMaterialCommand
        {
            TrainingScheduleId = 1,
            MaterialName = "Training Manual",
            FileName = "manual.pdf",
            FileStream = new MemoryStream(new byte[] { 1, 2, 3 }),
            ContentType = "application/pdf",
            FileSize = -5
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.FileSize)
            .WithErrorMessage("FileSize must be greater than 0.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UploadTrainingMaterialCommand
        {
            TrainingScheduleId = 1,
            MaterialName = "Training Manual",
            FileName = "manual.pdf",
            FileStream = new MemoryStream(new byte[] { 1, 2, 3 }),
            ContentType = "application/pdf",
            FileSize = 1024
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}