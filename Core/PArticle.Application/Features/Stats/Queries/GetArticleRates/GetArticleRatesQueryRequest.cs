using MediatR;
using PArticle.Application.Models.Stats;

namespace PArticle.Application.Features.Stats.Queries.GetArticleRates
{
	public class GetArticleRatesQueryRequest : DateLimitModel, IRequest<IList<GetArticleRatesQueryResponse>>
	{
		public GetArticleRatesQueryRequest(DateTime startDate, DateTime endDate, int count)
		{
			StartDate = startDate;
			EndDate = endDate;
			Count = count < 1 ? 1 : count;
		}
	}
}
