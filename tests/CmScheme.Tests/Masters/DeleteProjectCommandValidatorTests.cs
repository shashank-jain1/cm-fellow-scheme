using CmScheme.Masters.Application.Features.Projects.DeleteProject;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.Masters;

public class DeleteProjectCommandValidatorTests
{
    private readonly DeleteProjectCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ProjectId_Is_Zero()
    {
        var command = new DeleteProjectCommand
        {
            ProjectId = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectId)
            .WithErrorMessage("ProjectId must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_ProjectId_Is_Negative()
    {
        var command = new DeleteProjectCommand
        {
            ProjectId = -1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProjectId)
            .WithErrorMessage("ProjectId must be greater than 0.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_ProjectId_Is_Positive()
    {
        var command = new DeleteProjectCommand
        {
            ProjectId = 1
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.ProjectId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new DeleteProjectCommand
        {
            ProjectId = 1
        };

        var result = _validator.TestValidate(command);
        Assert.True(result.IsValid);
    }
}