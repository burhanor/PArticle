using MediatR;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Article.Queries.GetArticleByAuthor
{
	public class GetArticleByAuthorQueryRequest : FilterModel, IRequest<PaginationContainer<GetArticleByAuthorQueryResponse>>
	{
		public string Nickname { get; set; }
	}
}
