using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Domain.Views;

namespace PArticle.Application.Features.Article.Queries.GetMostViewedArticles
{
	public class GetMostViewedArticlesQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler(uow, httpContextAccessor, mapper),
		IRequestHandler<GetMostViewedArticlesQueryRequest, IList<GetMostViewedArticlesQueryResponse>>
	{
		public async Task<IList<GetMostViewedArticlesQueryResponse>> Handle(GetMostViewedArticlesQueryRequest request, CancellationToken cancellationToken)
		{
			if (request.Count <= 0)
			{
				request.Count = 5;
			}
			List<VwMostViewedArticle> results = uow.GetViewRepository<Domain.Views.VwMostViewedArticle>().Query()
				.OrderByDescending(x => x.DisplayOrder)
				.Take(request.Count)
				.ToList();
			results ??= [];
			IList<GetMostViewedArticlesQueryResponse> response = mapper.Map<IList<GetMostViewedArticlesQueryResponse>>(results);
			return response;
		}
	}
}
