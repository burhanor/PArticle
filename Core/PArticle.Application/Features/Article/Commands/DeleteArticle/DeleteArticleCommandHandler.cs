using AutoMapper;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Enums;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;

namespace PArticle.Application.Features.Article.Commands.DeleteArticle
{
	
	public class DeleteArticleCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Article, DeleteArticleCommandRequest>(uow, httpContextAccessor, mapper, Messages.Article.ARTICLE_DELETE_SUCCESS, Messages.Article.ARTICLE_DELETE_FAILED,rabbitMqService,Exchanges.Article)
	{
	}
}
