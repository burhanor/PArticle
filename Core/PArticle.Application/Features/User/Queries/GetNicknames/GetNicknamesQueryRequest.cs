using MediatR;

namespace PArticle.Application.Features.User.Queries.GetNicknames
{
	public class GetNicknamesQueryRequest(List<int> ids):IRequest<IList<GetNicknamesQueryResponse>>
	{
		public List<int> Ids { get; set; } = ids;
	}
}
