using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Enums;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Article.Commands.ResetArticleVotes
{
	public class ResetArticleVotesCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.ArticleVote>(uow, httpContextAccessor, mapper), IRequestHandler<ResetArticleVotesCommandRequest, ResponseContainer<Unit>>
	{
		public async Task<ResponseContainer<Unit>> Handle(ResetArticleVotesCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<Unit> response = new ResponseContainer<Unit>();
			response.Message = Messages.ArticleVote.ARTICLE_VOTE_RESET_FAILED;
			if (request.VoteType is null)
				writeRepository.Delete(m => m.ArticleId == request.ArticleId);
			else
				writeRepository.Delete(m => m.ArticleId == request.ArticleId && m.Vote == request.VoteType.Value);
			await uow.SaveChangesAsync(cancellationToken);
			response.Status = ResponseStatus.Success;
			response.Message = Messages.ArticleVote.ARTICLE_VOTE_RESET_SUCCESS;
			return response;
		}
	}
}
