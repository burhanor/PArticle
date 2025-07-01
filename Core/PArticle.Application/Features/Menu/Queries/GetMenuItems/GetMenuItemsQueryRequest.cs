using MediatR;
using PArticle.Application.Models;
using PArticle.Application.Models.Menu;

namespace PArticle.Application.Features.Menu.Queries.GetMenuItems
{
	public class GetMenuItemsQueryRequest : MenuFilterModel, IRequest<PaginationContainer<GetMenuItemsQueryResponse>>
	{
	}
}
