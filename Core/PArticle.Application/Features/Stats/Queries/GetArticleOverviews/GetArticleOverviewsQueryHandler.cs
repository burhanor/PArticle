using AutoMapper;
using Domain.Contracts.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Domain.Views;

namespace PArticle.Application.Features.Stats.Queries.GetArticleOverviews
{
	public class GetArticleOverviewsQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.Article>(uow, httpContextAccessor, mapper), IRequestHandler<GetArticleOverviewsQueryRequest, IList<GetArticleOverviewsQueryResponse>>
	{
		public async Task<IList<GetArticleOverviewsQueryResponse>> Handle(GetArticleOverviewsQueryRequest request, CancellationToken cancellationToken)
		{
			IList<VwArticleStatusCount> vwArticleStatusCounts = await uow.GetViewRepository<VwArticleStatusCount>().ToListAsync(cancellationToken);
			IList<GetArticleOverviewsQueryResponse> response = mapper.Map<IList<GetArticleOverviewsQueryResponse>>(vwArticleStatusCounts);

			var statusCounts = response.ToDictionary(r => r.Status);

			response = Enum.GetValues(typeof(Status))
				.Cast<Status>()
				.Select(status => statusCounts.TryGetValue(status, out var item)
					? item
					: new GetArticleOverviewsQueryResponse
					{
						Status = status,
						Count = 0
					})
				.ToList();

			return response;
		}
	}
}
