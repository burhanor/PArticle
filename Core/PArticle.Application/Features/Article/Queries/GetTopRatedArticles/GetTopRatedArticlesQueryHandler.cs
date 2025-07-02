using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Domain.Views;

namespace PArticle.Application.Features.Article.Queries.GetTopRatedArticles
{
	

	public class GetTopRatedArticlesQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler(uow, httpContextAccessor, mapper),
		IRequestHandler<GetTopRatedArticlesQueryRequest, IList<GetTopRatedArticlesQueryResponse>>
	{
		public async Task<IList<GetTopRatedArticlesQueryResponse>> Handle(GetTopRatedArticlesQueryRequest request, CancellationToken cancellationToken)
		{
			if (request.Count <= 0)
			{
				request.Count = 5;
			}
			List<VwTopRatedArticle> results = uow.GetViewRepository<Domain.Views.VwTopRatedArticle>().Query()
				.OrderByDescending(x => x.DisplayOrder)
				.Take(request.Count).ToList();
			results ??= [];
			IList<GetTopRatedArticlesQueryResponse> response = mapper.Map<IList<GetTopRatedArticlesQueryResponse>>(results);
			return response;
		}
	}
}
