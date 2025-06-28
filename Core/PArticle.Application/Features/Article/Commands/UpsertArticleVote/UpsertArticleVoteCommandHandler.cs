using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Enums;
using PArticle.Application.Helpers;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Article.Commands.UpsertArticleVote
{
	public class UpsertArticleVoteCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.ArticleVote>(uow, httpContextAccessor, mapper), IRequestHandler<UpsertArticleVoteCommandRequest, ResponseContainer<Unit>>
	{
		public async Task<ResponseContainer<Unit>> Handle(UpsertArticleVoteCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<Unit> response = await ValidationHelper.ValidateAsync<UpsertArticleVoteCommandRequest, Unit, UpsertArticleVoteCommandValidator>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;
			bool articleExist = await uow.GetReadRepository<Domain.Entities.Article>().ExistAsync(m => m.Id == request.ArticleId, cancellationToken);
			if (!articleExist)
			{
				response.Message = Messages.Article.ARTICLE_NOT_FOUND;
				return response;
			}
			request.IpAddress = ipAddress;
			Domain.Entities.ArticleVote? articleVote = await uow.GetReadRepository<Domain.Entities.ArticleVote>().GetAsync(m => m.ArticleId == request.ArticleId && m.IpAddress == request.IpAddress, cancellationToken: cancellationToken);
			articleVote ??= mapper.Map<Domain.Entities.ArticleVote>(request);
			if (articleVote.Id > 0)
			{
				articleVote.Vote = request.Vote;
				articleVote.VoteDate = DateTime.Now;
				writeRepository.Update(articleVote);
			}
			else
			{
				await writeRepository.AddAsync(articleVote, cancellationToken);
			}
			await uow.SaveChangesAsync(cancellationToken);
			if (articleVote.Id > 0)
			{
				response.Data = new Unit();
				response.Status = ResponseStatus.Success;
				response.Message = Messages.ArticleVote.ARTICLE_VOTE_ADDED;
			}
			else
			{
				response.Status = ResponseStatus.Failed;
				response.Message = Messages.ArticleVote.ARTICLE_VOTE_ADD_ERROR;
			}
			return response;
		}
	}
}
