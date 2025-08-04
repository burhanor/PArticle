using MediatR;

namespace PArticle.Application.Features.Article.Queries.ArticleIsExist
{
	public class ArticleIsExistQueryRequest(string slug) : IRequest<bool>
	{
		public string Slug { get; set; } = slug;
	}
}
