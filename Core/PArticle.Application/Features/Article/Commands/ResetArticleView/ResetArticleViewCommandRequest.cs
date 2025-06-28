using MediatR;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Article.Commands.ResetArticleView
{
	public class ResetArticleViewCommandRequest(int articleId) : IRequest<ResponseContainer<Unit>>
	{
		public int ArticleId { get; set; } = articleId;
	}
}
