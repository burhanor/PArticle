using MediatR;

namespace PArticle.Application.Features.Stats.Queries.GetTagOverviews
{
	public class GetTagOverviewsQueryRequest : IRequest<IList<GetTagOverviewsQueryResponse>>
	{
	}
}
