using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Enums;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Enums;
using PArticle.Application.Helpers;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Menu.Commands.UpdateMenuItem
{
	

	public class UpdateMenuItemCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.MenuItem>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<UpdateMenuItemCommandRequest, ResponseContainer<UpdateMenuItemCommandResponse>>
	{
		public async Task<ResponseContainer<UpdateMenuItemCommandResponse>> Handle(UpdateMenuItemCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<UpdateMenuItemCommandResponse> response = await ValidationHelper.ValidateAsync<UpdateMenuItemCommandRequest, UpdateMenuItemCommandResponse, UpdateMenuItemCommandValidator>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			
			Domain.Entities.MenuItem menuItem = mapper.Map<Domain.Entities.MenuItem>(request);
			if (!writeRepository.Update(menuItem))
			{
				response.Message = Messages.MenuItem.MENU_ITEM_NOT_FOUND;
				return response;
			}
			await uow.SaveChangesAsync(cancellationToken);

			await RabbitMqService.Publish(Exchanges.MenuItem, RoutingTypes.Updated, menuItem, cancellationToken);
			response.Data = mapper.Map<UpdateMenuItemCommandResponse>(menuItem);
			response.Message = Messages.MenuItem.MENU_ITEM_UPDATE_SUCCESS;
			response.Status = ResponseStatus.Success;
			return response;
		}
	}

}
