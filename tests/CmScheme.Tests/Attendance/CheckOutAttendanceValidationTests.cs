using CmScheme.AttendanceLeave.Application.Features.Attendance.CheckOutAttendance;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Attendance;

public class CheckOutAttendanceValidationTests
{
    private readonly CheckOutAttendanceCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ApplicantId_Is_Zero()
    {
        var command = new CheckOutAttendanceCommand
        {
            ApplicantId = 0,
            AttendanceDate = new DateTime(2026, 8, 10)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId)
            .WithErrorMessage("ApplicantId is required.");
    }

    [Fact]
    public void Should_Have_Error_When_ApplicantId_Is_Negative()
    {
        var command = new CheckOutAttendanceCommand
        {
            ApplicantId = -1,
            AttendanceDate = new DateTime(2026, 8, 10)
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId)
            .WithErrorMessage("ApplicantId is required.");
    }

    [Fact]
    public void Should_Have_Error_When_AttendanceDate_Is_Default()
    {
        var command = new CheckOutAttendanceCommand { ApplicantId = 1 };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.AttendanceDate)
            .WithErrorMessage("AttendanceDate is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CheckOutAttendanceCommand
        {
            ApplicantId = 1,
            AttendanceDate = new DateTime(2026, 8, 10)
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}