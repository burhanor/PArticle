using MediatR;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Article.Queries.GetArticleByCategory
{
	public class GetArticleByCategoryQueryRequest : FilterModel, IRequest<PaginationContainer<GetArticleByCategoryQueryResponse>>
	{
		public string? CategorySlug { get; set; }
	}
}
