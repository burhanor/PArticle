using MediatR;

namespace PArticle.Application.Features.Stats.Queries.GetUserOverviews
{
	public class GetUserOverviewsQueryRequest : IRequest<IList<GetUserOverviewsQueryResponse>>
	{
	}
}
