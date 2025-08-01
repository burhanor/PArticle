using MediatR;

namespace PArticle.Application.Features.Stats.Queries.GetTopCategories
{

	public class GetTopCategoriesQueryRequest(int limit) : IRequest<IList<GetTopCategoriesQueryResponse>>
	{
		public int Limit { get; set; } = limit < 1 ? 1 : limit;
	}
}
