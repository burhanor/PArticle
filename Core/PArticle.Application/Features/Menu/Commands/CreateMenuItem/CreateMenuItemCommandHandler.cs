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

namespace PArticle.Application.Features.Menu.Commands.CreateMenuItem
{
	

	public class CreateMenuItemCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.MenuItem>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<CreateMenuItemCommandRequest, ResponseContainer<CreateMenuItemCommandResponse>>
	{
		public async Task<ResponseContainer<CreateMenuItemCommandResponse>> Handle(CreateMenuItemCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<CreateMenuItemCommandResponse> response = await ValidationHelper.ValidateAsync<CreateMenuItemCommandRequest, CreateMenuItemCommandResponse, CreateMenuItemCommandValidator>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			

			Domain.Entities.MenuItem menuItem = mapper.Map<Domain.Entities.MenuItem>(request);
			await writeRepository.AddAsync(menuItem, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (menuItem.Id > 0)
			{
				await RabbitMqService.Publish(Exchanges.MenuItem, RoutingTypes.Created, menuItem, cancellationToken);
				response.Data = mapper.Map<CreateMenuItemCommandResponse>(menuItem);
				response.Message = Messages.MenuItem.MENU_ITEM_CREATE_SUCCESS;
				response.Status = ResponseStatus.Success;
			}
			else
			{
				response.Message = Messages.MenuItem.MENU_ITEM_CREATE_FAILED;
				response.Status = ResponseStatus.Failed;
			}
			return response;
		}
	}

}
