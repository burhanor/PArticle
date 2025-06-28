using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Enums;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Article.Commands.ResetArticleView
{
	public class ResetArticleViewCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.ArticleView>(uow, httpContextAccessor, mapper), IRequestHandler<ResetArticleViewCommandRequest, ResponseContainer<Unit>>
	{
		public async Task<ResponseContainer<Unit>> Handle(ResetArticleViewCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<Unit> response = new ResponseContainer<Unit>();
			response.Message = Messages.ArticleView.ARTICLE_VIEW_RESET_FAILED;
			writeRepository.Delete(m=>m.ArticleId == request.ArticleId);
			await uow.SaveChangesAsync(cancellationToken);
			response.Status = ResponseStatus.Success;
			response.Message = Messages.ArticleView.ARTICLE_VIEW_RESET_SUCCESS;
			return response;
		}
	}
}
