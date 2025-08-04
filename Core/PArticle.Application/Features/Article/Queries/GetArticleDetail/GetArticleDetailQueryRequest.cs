using MediatR;

namespace PArticle.Application.Features.Article.Queries.GetArticleDetail
{
	
	public class GetArticleDetailQueryRequest(string slug) :  IRequest<GetArticleDetailQueryResponse?>
	{
		public string Slug { get; set; } = slug;
	}
}
