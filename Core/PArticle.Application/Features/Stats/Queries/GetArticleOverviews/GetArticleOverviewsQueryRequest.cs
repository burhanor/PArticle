using MediatR;

namespace PArticle.Application.Features.Stats.Queries.GetArticleOverviews
{
	public class GetArticleOverviewsQueryRequest:IRequest<IList<GetArticleOverviewsQueryResponse>>
	{
	}
}
