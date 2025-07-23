using PArticle.Application.Concretes;

namespace PArticle.Application.Features.User.Queries.GetUser
{
	public class GetUserQueryRequest(int id) : GetByIdRequest<GetUserQueryResponse>(id)
	{
	}
}
