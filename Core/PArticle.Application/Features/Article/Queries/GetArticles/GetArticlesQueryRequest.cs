using MediatR;
using PArticle.Application.Models;
using PArticle.Application.Models.Article;

namespace PArticle.Application.Features.Article.Queries.GetArticles
{
	public class GetArticlesQueryRequest:ArticleFilterModel ,IRequest<PaginationContainer<GetArticlesQueryResponse>>
	{
	}
}
