using CmScheme.HelpDesk.Application.Features.KnowledgeBase.CreateArticle;
using FluentValidation.TestHelper;
using Xunit;

namespace CmScheme.Tests.HelpDesk;

public class CreateArticleValidationTests
{
    private readonly CreateArticleCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Title_Is_Empty()
    {
        var command = new CreateArticleCommand { Title = "" };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage("Title is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Title_Is_Null()
    {
        var command = new CreateArticleCommand { Title = null! };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage("Title is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_For_Other_Fields_When_Only_Title_Is_Validated()
    {
        var command = new CreateArticleCommand
        {
            Title = "How to reset a password",
            Content = "",
            Category = "",
            AuthorId = 0,
            AuthorName = ""
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateArticleCommand
        {
            Title = "How to reset a password",
            Content = "Step by step instructions...",
            Category = "Account",
            Tags = "password,reset",
            AuthorId = 1,
            AuthorName = "Support Team",
            IsPublished = true
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}