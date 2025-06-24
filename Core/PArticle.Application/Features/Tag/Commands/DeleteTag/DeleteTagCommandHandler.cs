using AutoMapper;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Enums;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;

namespace PArticle.Application.Features.Tag.Commands.DeleteTag
{
	

	public class DeleteTagCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Tag, DeleteTagCommandRequest>(uow, httpContextAccessor, mapper, Messages.Tag.TAG_DELETE_SUCCESS, Messages.Tag.TAG_DELETE_FAILED,rabbitMqService,Exchanges.Tag)
	{
	}
}
