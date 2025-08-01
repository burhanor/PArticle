using MediatR;

namespace PArticle.Application.Features.Stats.Queries.GetTopTags
{
	public class GetTopTagsQueryRequest(int limit) : IRequest<IList<GetTopTagsQueryResponse>>
	{
		public int Limit { get; set; } =  limit < 1 ? 1 : limit;
	}
}
