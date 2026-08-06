using CmScheme.AttendanceLeave.Application.Features.Attendance.MarkAttendance;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Attendance;

public class MarkAttendanceValidationTests
{
    private readonly MarkAttendanceCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_FaceImageBase64_Is_Empty()
    {
        var command = new MarkAttendanceCommand
        {
            ApplicantId = 1,
            AttendanceDate = DateTime.UtcNow,
            FaceImageBase64 = "",
            AttendanceStatus = "Present"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.FaceImageBase64)
            .WithErrorMessage("FaceImageBase64 is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_FaceImageBase64_Is_Provided()
    {
        var command = new MarkAttendanceCommand
        {
            ApplicantId = 1,
            AttendanceDate = DateTime.UtcNow,
            FaceImageBase64 = "validbase64string",
            AttendanceStatus = "Present"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.FaceImageBase64);
    }
}
