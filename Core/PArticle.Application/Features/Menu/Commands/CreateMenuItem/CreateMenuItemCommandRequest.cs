using MediatR;
using PArticle.Application.Models;
using PArticle.Application.Models.Menu;

namespace PArticle.Application.Features.Menu.Commands.CreateMenuItem
{
	public class CreateMenuItemCommandRequest : MenuDto, IRequest<ResponseContainer<CreateMenuItemCommandResponse>>
	{
	}
}
