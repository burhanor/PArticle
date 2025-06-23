using MediatR;
using PArticle.Application.Abstractions.Interfaces;

namespace PArticle.Application.Features.Article.Queries.GetArticle
{
	public class GetArticleQueryRequest(int id) : IId,IRequest<GetArticleQueryResponse?>
	{
		public int Id { get; set; } = id;
	}
}
