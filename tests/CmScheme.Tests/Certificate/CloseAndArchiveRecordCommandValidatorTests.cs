using CmScheme.Certificate.Application.Features.Exit.CloseAndArchiveRecord;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Certificate;

public class CloseAndArchiveRecordCommandValidatorTests
{
    private readonly CloseAndArchiveRecordCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ExitRecordId_Is_Zero()
    {
        var command = new CloseAndArchiveRecordCommand
        {
            ExitRecordId = 0,
            ApprovedBy = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ExitRecordId)
            .WithErrorMessage("Exit Record ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ApprovedBy_Is_Zero()
    {
        var command = new CloseAndArchiveRecordCommand
        {
            ExitRecordId = 1,
            ApprovedBy = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApprovedBy)
            .WithErrorMessage("Approved By is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ApprovedBy_Is_Negative()
    {
        var command = new CloseAndArchiveRecordCommand
        {
            ExitRecordId = 1,
            ApprovedBy = -5
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApprovedBy);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CloseAndArchiveRecordCommand
        {
            ExitRecordId = 1,
            ApprovedBy = 42
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}