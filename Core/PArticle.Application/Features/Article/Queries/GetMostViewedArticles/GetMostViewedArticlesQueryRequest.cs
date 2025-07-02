using MediatR;

namespace PArticle.Application.Features.Article.Queries.GetMostViewedArticles
{
	public class GetMostViewedArticlesQueryRequest(int count) : IRequest<IList<GetMostViewedArticlesQueryResponse>>
	{
		public int Count { get; set; } = count;
	}
}
