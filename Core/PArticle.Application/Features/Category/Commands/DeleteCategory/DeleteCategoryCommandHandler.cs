using AutoMapper;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Enums;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;

namespace PArticle.Application.Features.Category.Commands.DeleteCategory
{
	public class DeleteCategoryCommandHandler(IUow uow,IHttpContextAccessor httpContextAccessor,IMapper mapper,IRabbitMqService rabbitMqService):DeleteHandler<Domain.Entities.Category, DeleteCategoryCommandRequest>(uow,httpContextAccessor,mapper,Messages.Category.CATEGORY_DELETE_SUCCESS, Messages.Category.CATEGORY_DELETE_FAILED, rabbitMqService,Exchanges.Category)
	{
	}
}
