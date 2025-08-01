using MediatR;
using PArticle.Application.Models.Stats;

namespace PArticle.Application.Features.Stats.Queries.GetTopArticles
{
	public class GetTopArticlesQueryRequest: DateLimitModel,IRequest<IList<GetTopArticlesQueryResponse>>
	{
		public GetTopArticlesQueryRequest(DateTime startDate,DateTime endDate,int count)
		{
			StartDate = startDate;
			EndDate = endDate;
			Count = count < 1 ? 1 : count;
		}
	}
}
