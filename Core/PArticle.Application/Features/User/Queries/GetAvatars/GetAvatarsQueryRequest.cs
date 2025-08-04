using MediatR;

namespace PArticle.Application.Features.User.Queries.GetAvatars
{
	public class GetAvatarsQueryRequest(List<int> ids) : IRequest<IList<GetAvatarsQueryResponse>>
	{
		public List<int> Ids { get; set; } = ids;
	}
}
