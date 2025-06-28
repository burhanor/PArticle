using MediatR;

namespace PArticle.Application.Features.Article.Queries.GetArticleViewCount
{
	public class GetArticleViewCountQueryRequest(int articleId) : IRequest<GetArticleViewCountQueryResponse>
	{
		public int ArticleId { get; set; } = articleId;
	}
}
