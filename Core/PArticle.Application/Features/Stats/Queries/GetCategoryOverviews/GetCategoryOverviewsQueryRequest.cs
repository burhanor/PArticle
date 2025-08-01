using MediatR;

namespace PArticle.Application.Features.Stats.Queries.GetCategoryOverviews
{
	public class GetCategoryOverviewsQueryRequest : IRequest<IList<GetCategoryOverviewsQueryResponse>>
	{
	}
}
