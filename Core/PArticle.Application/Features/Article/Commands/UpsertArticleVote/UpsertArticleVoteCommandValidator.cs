using FluentValidation;
using PArticle.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Article.Commands.UpsertArticleVote
{
	internal class UpsertArticleVoteCommandValidator:AbstractValidator<UpsertArticleVoteCommandRequest>
	{
		public UpsertArticleVoteCommandValidator()
		{
			RuleFor(m => m.ArticleId)
				.GreaterThan(0).WithMessage(Messages.Article.ARTICLE_NOT_FOUND);
			RuleFor(m => m.Vote)
				.IsInEnum().WithMessage(Messages.ArticleVote.ARTICLE_VOTE_TYPE_INVALID);
		}
	}
}
