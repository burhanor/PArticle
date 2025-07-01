using AutoMapper;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Enums;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;

namespace PArticle.Application.Features.Menu.Commands.DeleteMenuItem
{
	

	public class DeleteMenuItemCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.MenuItem, DeleteMenuItemCommandRequest>(uow, httpContextAccessor, mapper, Messages.MenuItem.MENU_ITEM_DELETE_SUCCESS, Messages.MenuItem.MENU_ITEM_DELETE_FAILED, rabbitMqService, Exchanges.MenuItem)
	{
	}
}
