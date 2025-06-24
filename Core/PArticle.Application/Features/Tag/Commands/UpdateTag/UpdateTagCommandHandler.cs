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

namespace PArticle.Application.Features.Tag.Commands.UpdateTag
{


	public class UpdateTagCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Tag>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<UpdateTagCommandRequest, ResponseContainer<UpdateTagCommandResponse>>
	{
		public async Task<ResponseContainer<UpdateTagCommandResponse>> Handle(UpdateTagCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<UpdateTagCommandResponse> response = await ValidationHelper.ValidateAsync<UpdateTagCommandRequest, UpdateTagCommandResponse, UpdateTagCommandValidator>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool nameUnique = await readRepository.UniqueAsync(m => m.Name == request.Name && m.Id != request.Id, cancellationToken);
			if (!nameUnique)
			{
				response.AddValidationError(nameof(request.Name), Messages.Tag.TAG_NAME_ALREADY_EXIST);
				return response;
			}
			bool slugUnique = await readRepository.UniqueAsync(m => m.Slug == request.Slug && m.Id != request.Id, cancellationToken);
			if (!slugUnique)
			{
				response.AddValidationError(nameof(request.Slug), Messages.Tag.TAG_SLUG_ALREADY_EXIST);
				return response;
			}
			Domain.Entities.Tag tag = mapper.Map<Domain.Entities.Tag>(request);
			if (!writeRepository.Update(tag))
			{
				response.Message = Messages.Tag.TAG_NOT_FOUND;
				return response;
			}
			await uow.SaveChangesAsync(cancellationToken);

			await RabbitMqService.Publish(Exchanges.Tag, RoutingTypes.Updated, tag, cancellationToken);
			response.Data = mapper.Map<UpdateTagCommandResponse>(tag);
			response.Message = Messages.Tag.TAG_UPDATE_SUCCESS;
			response.Status = ResponseStatus.Success;
			return response;
		}
	}

}
