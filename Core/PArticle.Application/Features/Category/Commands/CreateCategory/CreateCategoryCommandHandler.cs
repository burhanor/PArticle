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

namespace PArticle.Application.Features.Category.Commands.CreateCategory
{
	public class CreateCategoryCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Category>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<CreateCategoryCommandRequest, ResponseContainer<CreateCategoryCommandResponse>>
	{
		public async Task<ResponseContainer<CreateCategoryCommandResponse>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<CreateCategoryCommandResponse> response = await ValidationHelper.ValidateAsync<CreateCategoryCommandRequest,CreateCategoryCommandResponse,CreateCategoryCommandValidator>(request,cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool nameUnique = await readRepository.UniqueAsync(m => m.Name == request.Name, cancellationToken);
			if (!nameUnique)
			{
				response.AddValidationError(nameof(request.Name), Messages.Category.CATEGORY_NAME_ALREADY_EXIST);
				return response;
			}

			bool slugUnique = await readRepository.UniqueAsync(m => m.Slug == request.Slug, cancellationToken);
			if (!slugUnique)
			{
				response.AddValidationError(nameof(request.Slug), Messages.Category.CATEGORY_SLUG_ALREADY_EXIST);
				return response;
			}

			Domain.Entities.Category category = mapper.Map<Domain.Entities.Category>(request);
			await writeRepository.AddAsync(category, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (category.Id > 0)
			{
				await RabbitMqService.Publish(Exchanges.Category,  RoutingTypes.Created,  category, cancellationToken);
				response.Data = mapper.Map<CreateCategoryCommandResponse>(category);
				response.Message = Messages.Category.CATEGORY_CREATE_SUCCESS;
				response.Status = ResponseStatus.Success;
			}
			else
			{
				response.Message = Messages.Category.CATEGORY_CREATE_FAILED;
				response.Status = ResponseStatus.Failed;
			}

			return response;
		}
	}
}
