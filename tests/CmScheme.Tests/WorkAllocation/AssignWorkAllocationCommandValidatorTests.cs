using CmScheme.WorkAllocation.Application.Features.WorkAllocation.AssignWorkAllocation;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.WorkAllocation;

public class AssignWorkAllocationCommandValidatorTests
{
    private readonly AssignWorkAllocationCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_WorkAllocationId_Is_Zero()
    {
        var command = new AssignWorkAllocationCommand
        {
            WorkAllocationId = 0,
            AssignedToUserId = 5
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkAllocationId)
            .WithErrorMessage("WorkAllocationId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_WorkAllocationId_Is_Negative()
    {
        var command = new AssignWorkAllocationCommand
        {
            WorkAllocationId = -3,
            AssignedToUserId = 5
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkAllocationId)
            .WithErrorMessage("WorkAllocationId must be greater than 0.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_AssignedToUserId_Is_Zero()
    {
        var command = new AssignWorkAllocationCommand
        {
            WorkAllocationId = 10,
            AssignedToUserId = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.AssignedToUserId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new AssignWorkAllocationCommand
        {
            WorkAllocationId = 10,
            AssignedToUserId = 5
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}