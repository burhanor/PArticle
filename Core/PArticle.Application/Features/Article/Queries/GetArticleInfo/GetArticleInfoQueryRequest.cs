using MediatR;

namespace PArticle.Application.Features.Article.Queries.GetArticleInfo
{
	public class GetArticleInfoQueryRequest(List<int> ids) : IRequest<IList<GetArticleInfoQueryResponse>>
	{
		public List<int> Ids { get; set; } = ids;
	}
}
