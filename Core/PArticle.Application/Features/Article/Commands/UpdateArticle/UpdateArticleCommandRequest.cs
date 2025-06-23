using MediatR;
using PArticle.Application.Abstractions.Interfaces;
using PArticle.Application.Models;
using PArticle.Application.Models.Article;

namespace PArticle.Application.Features.Article.Commands.UpdateArticle
{
	public class UpdateArticleCommandRequest:ArticleUpdateModel,IId,IRequest<ResponseContainer<UpdateArticleCommandResponse>>
	{
		public int Id { get; set; }
	}
}
