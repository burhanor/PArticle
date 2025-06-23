using FluentValidation;
using PArticle.Application.Constants;

namespace PArticle.Application.Features.Article.Commands.UpdateArticle
{
	internal class UpdateArticleCommandValidator:AbstractValidator<UpdateArticleCommandRequest>
	{
		public UpdateArticleCommandValidator()
		{
			RuleFor(m => m.Title)
				.NotEmpty().WithMessage(Messages.Article.ARTICLE_TITLE_REQUIRED);
			RuleFor(m => m.Slug)
				.NotEmpty().WithMessage(Messages.Article.ARTICLE_SLUG_REQUIRED)
				.MaximumLength(50).WithMessage(Messages.Article.ARTICLE_SLUG_MAX_LENGTH);
			RuleFor(m => m.Content)
				.NotEmpty().WithMessage(Messages.Article.ARTICLE_CONTENT_REQUIRED);
			RuleFor(m => m.Status)
				.IsInEnum().WithMessage(Messages.Article.ARTICLE_STATUS_INVALID);
		}
	}
}
