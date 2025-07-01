using PArticle.Application.Concretes;

namespace PArticle.Application.Features.Menu.Queries.GetMenuItem
{
	public class GetMenuItemQueryRequest(int id) : GetByIdRequest<GetMenuItemQueryResponse>(id)
	{
	}
}
