using MediatR;
using PArticle.Application.Abstractions.Interfaces;
using PArticle.Application.Models;
using PArticle.Application.Models.Menu;

namespace PArticle.Application.Features.Menu.Commands.UpdateMenuItem
{
	

	public class UpdateMenuItemCommandRequest : MenuDto, IRequest<ResponseContainer<UpdateMenuItemCommandResponse>>, IId
	{
		public int Id { get; set; }
	}
}
