using CmScheme.WorkAllocation.Application.Features.TaskDependency.AddTaskDependency;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.WorkAllocation;

public class AddTaskDependencyCommandValidatorTests
{
    private readonly AddTaskDependencyCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_WorkAllocationId_Is_Zero()
    {
        var command = new AddTaskDependencyCommand
        {
            WorkAllocationId = 0,
            DependsOnWorkAllocationId = 3
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkAllocationId)
            .WithErrorMessage("WorkAllocationId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_WorkAllocationId_Is_Negative()
    {
        var command = new AddTaskDependencyCommand
        {
            WorkAllocationId = -2,
            DependsOnWorkAllocationId = 3
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.WorkAllocationId)
            .WithErrorMessage("WorkAllocationId must be greater than 0.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_DependsOnWorkAllocationId_Is_Zero()
    {
        var command = new AddTaskDependencyCommand
        {
            WorkAllocationId = 10,
            DependsOnWorkAllocationId = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.DependsOnWorkAllocationId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new AddTaskDependencyCommand
        {
            WorkAllocationId = 10,
            DependsOnWorkAllocationId = 3
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}