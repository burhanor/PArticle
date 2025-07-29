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
using PArticle.Application.Helpers.FeatureHelpers;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Category.Commands.UpdateCategory
{
	public class UpdateCategoryCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Category>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<UpdateCategoryCommandRequest, ResponseContainer<UpdateCategoryCommandResponse>>
	{
		public async Task<ResponseContainer<UpdateCategoryCommandResponse>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<UpdateCategoryCommandResponse> response = await ValidationHelper.ValidateAsync<UpdateCategoryCommandRequest, UpdateCategoryCommandResponse, UpdateCategoryCommandValidator>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool nameUnique = await readRepository.UniqueAsync(m => m.Name == request.Name && m.Id != request.Id, cancellationToken);
			if (!nameUnique)
			{
				response.AddValidationError(nameof(request.Name), Messages.Category.CATEGORY_NAME_ALREADY_EXIST);
				return response;
			}
			bool slugUnique = await readRepository.UniqueAsync(m => m.Slug == request.Slug && m.Id != request.Id, cancellationToken);
			if (!slugUnique)
			{
				response.AddValidationError(nameof(request.Slug), Messages.Category.CATEGORY_SLUG_ALREADY_EXIST);
				return response;
			}
			Domain.Entities.Category category = mapper.Map<Domain.Entities.Category>(request);
			if (!writeRepository.Update(category))
			{
				response.Message = Messages.Category.CATEGORY_NOT_FOUND;
				return response;
			}
			await uow.SaveChangesAsync(cancellationToken);

			var articleIds = await uow
				.GetReadRepository<Domain.Entities.ArticleCategory>()
				.GetListAsync(
				predicate: m => m.CategoryId == category.Id,
				select: m => m.ArticleId,
				cancellationToken: cancellationToken
				);
			await ArticleHelper.UpdateArticles(articleIds, uow, httpContextAccessor, mapper, rabbitMqService, cancellationToken);


			await RabbitMqService.Publish(Exchanges.Category, RoutingTypes.Updated, category, cancellationToken);

			response.Data = mapper.Map<UpdateCategoryCommandResponse>(category);
			response.Message = Messages.Category.CATEGORY_UPDATE_SUCCESS;
			response.Status = ResponseStatus.Success;
			return response;
		}
	}
}
