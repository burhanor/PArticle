using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Repositories;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.Article.Queries.GetArticleVotes
{
	public class GetArticleVotesQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler(uow, httpContextAccessor, mapper), IRequestHandler<GetArticleVotesQueryRequest, IList<GetArticleVotesQueryResponse>>
	{
		public async Task<IList<GetArticleVotesQueryResponse>> Handle(GetArticleVotesQueryRequest request, CancellationToken cancellationToken)
		{
			IViewRepository<Domain.Views.VwArticleVote> viewRepository = uow.GetViewRepository<Domain.Views.VwArticleVote>();
			IList<Domain.Views.VwArticleVote> votes;
			if (request.Vote is null)
				 votes = await viewRepository.ToListAsync(m => m.ArticleId == request.ArticleId, cancellationToken);
			else
				 votes = await viewRepository.ToListAsync(m => m.ArticleId == request.ArticleId && m.Vote == request.Vote, cancellationToken);
				
			return mapper.Map<IList<GetArticleVotesQueryResponse>>(votes);

		}
	}
}
