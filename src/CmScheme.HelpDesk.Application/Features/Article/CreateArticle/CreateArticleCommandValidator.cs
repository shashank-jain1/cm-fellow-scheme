using FluentValidation;

namespace CmScheme.HelpDesk.Application.Features.KnowledgeBase.CreateArticle;

public sealed class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.");
    }
}
