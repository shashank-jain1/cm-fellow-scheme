using CmScheme.AttendanceLeave.Application.Features.Attendance.MarkAttendance;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Attendance;

public class MarkAttendanceValidationTests
{
    private readonly MarkAttendanceCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_FaceMatchPercentage_Below_80()
    {
        var command = new MarkAttendanceCommand
        {
            ApplicantId = 1,
            AttendanceDate = DateTime.UtcNow,
            CaptureFacePath = "/faces/face1.jpg",
            FaceMatchPercentage = 75m,
            FaceVerificationStatus = "Verified",
            AttendanceStatus = "Present"
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.FaceMatchPercentage)
            .WithErrorMessage("Face match must be at least 80% for attendance verification.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_FaceMatchPercentage_Is_80_Or_Above()
    {
        var command = new MarkAttendanceCommand
        {
            ApplicantId = 1,
            AttendanceDate = DateTime.UtcNow,
            CaptureFacePath = "/faces/face1.jpg",
            FaceMatchPercentage = 85m,
            FaceVerificationStatus = "Verified",
            AttendanceStatus = "Present"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.FaceMatchPercentage);
    }
}
