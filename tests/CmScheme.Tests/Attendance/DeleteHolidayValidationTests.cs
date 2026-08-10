using CmScheme.AttendanceLeave.Application.Features.Holiday.DeleteHoliday;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Attendance;

public class DeleteHolidayValidationTests
{
    private readonly DeleteHolidayCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_HolidayId_Is_Zero()
    {
        var command = new DeleteHolidayCommand { HolidayId = 0 };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.HolidayId)
            .WithErrorMessage("Holiday ID is required.");
    }

    [Fact]
    public void Should_Have_Error_When_HolidayId_Is_Negative()
    {
        var command = new DeleteHolidayCommand { HolidayId = -1 };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.HolidayId)
            .WithErrorMessage("Holiday ID is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_HolidayId_Is_Positive()
    {
        var command = new DeleteHolidayCommand { HolidayId = 1 };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.HolidayId);
    }

    [Fact]
    public void Should_Be_Valid_For_Positive_HolidayId()
    {
        var command = new DeleteHolidayCommand { HolidayId = 5 };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}