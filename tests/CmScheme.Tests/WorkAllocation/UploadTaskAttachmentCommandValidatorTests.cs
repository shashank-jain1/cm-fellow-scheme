using CmScheme.WorkAllocation.Application.Features.TaskAttachments.UploadTaskAttachment;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace CmScheme.Tests.WorkAllocation;

public class UploadTaskAttachmentCommandValidatorTests
{
    private readonly UploadTaskAttachmentCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_WorkAllocationId_Is_Zero()
    {
        var command = new UploadTaskAttachmentCommand
        {
            WorkAllocationId = 0,
            File = new Mock<IFormFile>().Object
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkAllocationId)
            .WithErrorMessage("WorkAllocationId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_File_Is_Null()
    {
        var command = new UploadTaskAttachmentCommand
        {
            WorkAllocationId = 1,
            File = null!
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.File)
            .WithErrorMessage("File is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_File_Is_Provided()
    {
        var command = new UploadTaskAttachmentCommand
        {
            WorkAllocationId = 1,
            File = new Mock<IFormFile>().Object
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.File);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UploadTaskAttachmentCommand
        {
            WorkAllocationId = 1,
            File = new Mock<IFormFile>().Object
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}