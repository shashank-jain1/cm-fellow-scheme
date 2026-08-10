using CmScheme.Performance.Application.Features.Performance.ReviewCycle.CreateReviewCycle;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Performance;

public class CreateReviewCycleCommandValidatorTests
{
    private readonly CreateReviewCycleCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_CycleName_Is_Empty()
    {
        var command = new CreateReviewCycleCommand
        {
            CycleName = "",
            StartDate = new DateTime(2026, 1, 1),
            EndDate = new DateTime(2026, 6, 30)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CycleName)
            .WithErrorMessage("CycleName is required.");
    }

    [Fact]
    public void Should_Have_Error_When_CycleName_Exceeds_MaxLength()
    {
        var command = new CreateReviewCycleCommand
        {
            CycleName = new string('c', 101),
            StartDate = new DateTime(2026, 1, 1),
            EndDate = new DateTime(2026, 6, 30)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CycleName)
            .WithErrorMessage("CycleName must not exceed 100 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_StartDate_Is_Default()
    {
        var command = new CreateReviewCycleCommand
        {
            CycleName = "H1 2026",
            StartDate = default,
            EndDate = new DateTime(2026, 6, 30)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StartDate)
            .WithErrorMessage("StartDate is required.");
    }

    [Fact]
    public void Should_Have_Error_When_EndDate_Is_Default()
    {
        var command = new CreateReviewCycleCommand
        {
            CycleName = "H1 2026",
            StartDate = new DateTime(2026, 1, 1),
            EndDate = default
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithErrorMessage("EndDate is required.");
    }

    [Fact]
    public void Should_Have_Error_When_EndDate_Is_Before_StartDate()
    {
        var command = new CreateReviewCycleCommand
        {
            CycleName = "H1 2026",
            StartDate = new DateTime(2026, 6, 30),
            EndDate = new DateTime(2026, 1, 1)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithErrorMessage("EndDate must be after StartDate.");
    }

    [Fact]
    public void Should_Have_Error_When_EndDate_Equals_StartDate()
    {
        var command = new CreateReviewCycleCommand
        {
            CycleName = "H1 2026",
            StartDate = new DateTime(2026, 1, 1),
            EndDate = new DateTime(2026, 1, 1)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithErrorMessage("EndDate must be after StartDate.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateReviewCycleCommand
        {
            CycleName = "H1 2026",
            StartDate = new DateTime(2026, 1, 1),
            EndDate = new DateTime(2026, 6, 30)
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}