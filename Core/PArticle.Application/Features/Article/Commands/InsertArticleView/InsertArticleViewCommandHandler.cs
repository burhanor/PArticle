using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Enums;
using PArticle.Application.Helpers;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Article.Commands.InsertArticleView
{
	public class InsertArticleViewCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.ArticleView>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<InsertArticleViewCommandRequest, ResponseContainer<InsertArticleViewCommandResponse>>
	{
		public async Task<ResponseContainer<InsertArticleViewCommandResponse>> Handle(InsertArticleViewCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<InsertArticleViewCommandResponse> response = await ValidationHelper.ValidateAsync<InsertArticleViewCommandRequest, InsertArticleViewCommandResponse, InsertArticleViewCommandValidator>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool articleExist = await uow.GetReadRepository<Domain.Entities.Article>().ExistAsync(m => m.Id == request.ArticleId, cancellationToken);
			if(!articleExist)
			{
				response.Message = Messages.Article.ARTICLE_NOT_FOUND;
				return response;
			}
			request.IpAddress = ipAddress;
			Domain.Entities.ArticleView articleView = mapper.Map<Domain.Entities.ArticleView>(request);
		 	await writeRepository.AddAsync(articleView, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (articleView.Id > 0)
			{
				Domain.Views.VwArticleView? viewCount = uow.GetViewRepository<Domain.Views.VwArticleView>().Query()
					.Where(m => m.ArticleId == articleView.ArticleId).FirstOrDefault();
				if (viewCount != null)
				{
					response.Data = new InsertArticleViewCommandResponse()
					{
						ViewCount = viewCount.TotalViewCount
					};
				}
				response.Status = ResponseStatus.Success;
				response.Message = Messages.ArticleView.ARTICLE_VIEW_ADDED;
			}
			else
			{
				response.Status = ResponseStatus.Failed;
				response.Message = Messages.ArticleView.ARTICLE_VIEW_ADD_ERROR;
			}

			return response;
		}
	}
}
