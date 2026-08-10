using CmScheme.Dashboard.Application.Features.Dashboard.ExportDashboardToPdf;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Dashboard;

public class ExportDashboardToPdfValidationTests
{
    private readonly ExportDashboardToPdfCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_EndDate_Is_Before_StartDate()
    {
        var command = new ExportDashboardToPdfCommand
        {
            StartDate = new DateTime(2026, 8, 10),
            EndDate = new DateTime(2026, 8, 1)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithErrorMessage("End date must be after start date.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_EndDate_Equals_StartDate()
    {
        var command = new ExportDashboardToPdfCommand
        {
            StartDate = new DateTime(2026, 8, 10),
            EndDate = new DateTime(2026, 8, 10)
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.EndDate);
    }

    [Fact]
    public void Should_Not_Have_Error_When_EndDate_Is_After_StartDate()
    {
        var command = new ExportDashboardToPdfCommand
        {
            StartDate = new DateTime(2026, 8, 1),
            EndDate = new DateTime(2026, 8, 10)
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.EndDate);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Only_StartDate_Is_Provided()
    {
        var command = new ExportDashboardToPdfCommand { StartDate = new DateTime(2026, 8, 1) };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_No_Dates_Are_Provided()
    {
        var command = new ExportDashboardToPdfCommand();

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}