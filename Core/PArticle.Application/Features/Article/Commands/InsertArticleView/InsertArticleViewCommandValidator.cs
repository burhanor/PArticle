using FluentValidation;
using PArticle.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Article.Commands.InsertArticleView
{
	public class InsertArticleViewCommandValidator:AbstractValidator<InsertArticleViewCommandRequest>
	{
		public InsertArticleViewCommandValidator()
		{
			RuleFor(m => m.ArticleId)
				.GreaterThan(0).WithMessage(Messages.Article.ARTICLE_NOT_FOUND);
		}
	}
}
