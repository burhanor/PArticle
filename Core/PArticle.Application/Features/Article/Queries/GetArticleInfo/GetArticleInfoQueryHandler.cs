using AutoMapper;
using Domain.Contracts.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.Article.Queries.GetArticleInfo
{
	public class GetArticleInfoQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler(uow, httpContextAccessor, mapper), IRequestHandler<GetArticleInfoQueryRequest, IList<GetArticleInfoQueryResponse>>
	{
		public async Task<IList<GetArticleInfoQueryResponse>> Handle(GetArticleInfoQueryRequest request, CancellationToken cancellationToken)
		{
			var articleViewRepository = uow.GetViewRepository<Domain.Views.VwArticleView>();
			var articleVoteRepository = uow.GetViewRepository<Domain.Views.VwArticleVote>();
			var views = await articleViewRepository.ToListAsync(x => request.Ids.Contains(x.ArticleId), cancellationToken);
			var votes = await articleVoteRepository.ToListAsync(x => request.Ids.Contains(x.ArticleId), cancellationToken);

			List<GetArticleInfoQueryResponse> response = request.Ids.Select(m => new GetArticleInfoQueryResponse()
			{
				ArticleId = m,
				LikeCount = votes.FirstOrDefault(x => x.ArticleId == m && x.Vote == ArticleVote.Like)?.TotalVoteCount ?? 0,
				DislikeCount = votes.FirstOrDefault(x => x.ArticleId == m && x.Vote == ArticleVote.Dislike)?.TotalVoteCount ?? 0,
				ViewCount = views.FirstOrDefault(x => x.ArticleId == m)?.TotalViewCount ?? 0,
			}).ToList();





			return response;

		}
	}
}
