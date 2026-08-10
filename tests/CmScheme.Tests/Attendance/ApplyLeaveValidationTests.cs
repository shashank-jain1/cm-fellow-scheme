using CmScheme.AttendanceLeave.Application.Features.Leave.ApplyLeave;
using CmScheme.AttendanceLeave.Core.Entities;
using CmScheme.Tests;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Attendance;

public class ApplyLeaveValidationTests
{
    private readonly DateTime _fromDate = new(2026, 8, 1);
    private readonly DateTime _toDate = new(2026, 8, 2);

    private ApplyLeaveCommand BuildValidCommand() => new()
    {
        ApplicantId = 1,
        LeaveType = "Casual",
        FromDate = _fromDate,
        ToDate = _toDate,
        NumberOfDays = 2,
        HalfDayFullDay = "Full",
        LeaveReason = "Personal work",
        ReportingManagerName = "Manager",
        CreatedBy = "system"
    };

    [Fact]
    public async Task Should_Not_Have_Error_For_Valid_Command_With_Empty_Context()
    {
        using var dbContext = TestDbContext.CreateAttendanceLeave();
        var validator = new ApplyLeaveCommandValidator(dbContext);

        var result = await validator.TestValidateAsync(BuildValidCommand());

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_Conflicting_Leave_Already_Exists()
    {
        using var dbContext = TestDbContext.CreateAttendanceLeave(ctx =>
        {
            ctx.LeaveApplications.Add(new LeaveApplication
            {
                ApplicantId = 1,
                LeaveType = "Casual",
                FromDate = _fromDate,
                ToDate = _toDate.AddDays(3),
                NumberOfDays = 4,
                HalfDayFullDay = "Full",
                LeaveReason = "Existing leave",
                ReportingManagerName = "Manager",
                Status = "Pending",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "system"
            });
            ctx.SaveChanges();
        });
        var validator = new ApplyLeaveCommandValidator(dbContext);

        var result = await validator.TestValidateAsync(BuildValidCommand());

        result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("Leave application already exists for the selected date range. Same-date leave cannot be applied twice.");
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_Conflict_Exists_For_Another_Applicant()
    {
        using var dbContext = TestDbContext.CreateAttendanceLeave(ctx =>
        {
            ctx.LeaveApplications.Add(new LeaveApplication
            {
                ApplicantId = 99,
                LeaveType = "Casual",
                FromDate = _fromDate,
                ToDate = _toDate,
                NumberOfDays = 2,
                HalfDayFullDay = "Full",
                LeaveReason = "Other applicant leave",
                ReportingManagerName = "Manager",
                Status = "Pending",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "system"
            });
            ctx.SaveChanges();
        });
        var validator = new ApplyLeaveCommandValidator(dbContext);

        var result = await validator.TestValidateAsync(BuildValidCommand());

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_ApplicantId_Is_Zero()
    {
        using var dbContext = TestDbContext.CreateAttendanceLeave();
        var validator = new ApplyLeaveCommandValidator(dbContext);

        var command = BuildValidCommand() with { ApplicantId = 0 };

        var result = await validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.ApplicantId)
            .WithErrorMessage("ApplicantId must be greater than 0.");
    }

    [Fact]
    public async Task Should_Have_Error_When_ToDate_Is_Before_FromDate()
    {
        using var dbContext = TestDbContext.CreateAttendanceLeave();
        var validator = new ApplyLeaveCommandValidator(dbContext);

        var command = BuildValidCommand() with { ToDate = _fromDate.AddDays(-1) };

        var result = await validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.ToDate)
            .WithErrorMessage("ToDate must be on or after FromDate.");
    }

    [Fact]
    public async Task Should_Have_Error_When_NumberOfDays_Is_Zero()
    {
        using var dbContext = TestDbContext.CreateAttendanceLeave();
        var validator = new ApplyLeaveCommandValidator(dbContext);

        var command = BuildValidCommand() with { NumberOfDays = 0 };

        var result = await validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.NumberOfDays)
            .WithErrorMessage("NumberOfDays must be greater than 0.");
    }

    [Fact]
    public async Task Should_Have_Error_When_LeaveReason_Is_Empty()
    {
        using var dbContext = TestDbContext.CreateAttendanceLeave();
        var validator = new ApplyLeaveCommandValidator(dbContext);

        var command = BuildValidCommand() with { LeaveReason = "" };

        var result = await validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.LeaveReason)
            .WithErrorMessage("LeaveReason is required.");
    }
}