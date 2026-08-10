using CmScheme.WorkAllocation.Application.Features.WorkAllocation.DeactivateWorkAllocation;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.WorkAllocation;

public class DeactivateWorkAllocationCommandValidatorTests
{
    private readonly DeactivateWorkAllocationCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_WorkAllocationId_Is_Zero()
    {
        var command = new DeactivateWorkAllocationCommand
        {
            WorkAllocationId = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkAllocationId)
            .WithErrorMessage("WorkAllocationId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_WorkAllocationId_Is_Negative()
    {
        var command = new DeactivateWorkAllocationCommand
        {
            WorkAllocationId = -5
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkAllocationId)
            .WithErrorMessage("WorkAllocationId must be greater than 0.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_WorkAllocationId_Is_Greater_Than_Zero()
    {
        var command = new DeactivateWorkAllocationCommand
        {
            WorkAllocationId = 10
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.WorkAllocationId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new DeactivateWorkAllocationCommand
        {
            WorkAllocationId = 10
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}