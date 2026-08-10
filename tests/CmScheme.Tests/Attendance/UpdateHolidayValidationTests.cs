using CmScheme.AttendanceLeave.Application.Features.Holiday.UpdateHoliday;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Attendance;

public class UpdateHolidayValidationTests
{
    private readonly UpdateHolidayCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_HolidayId_Is_Zero()
    {
        var command = new UpdateHolidayCommand
        {
            HolidayId = 0,
            HolidayName = "Independence Day",
            HolidayDate = new DateTime(2026, 8, 15)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.HolidayId)
            .WithErrorMessage("Holiday ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_HolidayName_Is_Empty()
    {
        var command = new UpdateHolidayCommand
        {
            HolidayId = 1,
            HolidayName = "",
            HolidayDate = new DateTime(2026, 8, 15)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.HolidayName)
            .WithErrorMessage("Holiday name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_HolidayName_Exceeds_Maximum_Length()
    {
        var command = new UpdateHolidayCommand
        {
            HolidayId = 1,
            HolidayName = new string('a', 201),
            HolidayDate = new DateTime(2026, 8, 15)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.HolidayName)
            .WithErrorMessage("Holiday name must not exceed 200 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_HolidayDate_Is_Default()
    {
        var command = new UpdateHolidayCommand { HolidayId = 1, HolidayName = "Independence Day" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.HolidayDate)
            .WithErrorMessage("Holiday date is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new UpdateHolidayCommand
        {
            HolidayId = 1,
            HolidayName = "Independence Day",
            HolidayDate = new DateTime(2026, 8, 15),
            Description = "Updated national holiday",
            IsOptional = false
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}