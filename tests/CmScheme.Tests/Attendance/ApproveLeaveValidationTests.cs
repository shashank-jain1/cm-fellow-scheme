using CmScheme.AttendanceLeave.Application.Features.Leave.ApproveLeave;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Attendance;

public class ApproveLeaveValidationTests
{
    private readonly ApproveLeaveCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_LeaveApplicationId_Is_Zero()
    {
        var command = new ApproveLeaveCommand { LeaveApplicationId = 0, Status = "Approved", Remarks = "OK" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.LeaveApplicationId)
            .WithErrorMessage("LeaveApplicationId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_Status_Is_Empty()
    {
        var command = new ApproveLeaveCommand { LeaveApplicationId = 1, Status = "", Remarks = "OK" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("Status is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Status_Exceeds_Maximum_Length()
    {
        var command = new ApproveLeaveCommand { LeaveApplicationId = 1, Status = new string('a', 51), Remarks = "OK" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("Status must not exceed 50 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Remarks_Is_Empty()
    {
        var command = new ApproveLeaveCommand { LeaveApplicationId = 1, Status = "Approved", Remarks = "" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Remarks)
            .WithErrorMessage("Remarks is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new ApproveLeaveCommand
        {
            LeaveApplicationId = 1,
            Status = "Approved",
            Remarks = "Verified documents, approved."
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}