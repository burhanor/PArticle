using AutoMapper;
using Domain.Contracts.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Domain.Views;

namespace PArticle.Application.Features.Stats.Queries.GetCategoryOverviews
{
	
	public class GetCategoryOverviewsQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.Category>(uow, httpContextAccessor, mapper), IRequestHandler<GetCategoryOverviewsQueryRequest, IList<GetCategoryOverviewsQueryResponse>>
	{
		public async Task<IList<GetCategoryOverviewsQueryResponse>> Handle(GetCategoryOverviewsQueryRequest request, CancellationToken cancellationToken)
		{
			IList<VwCategoryStatusCount> vwCategoryStatusCounts = await uow.GetViewRepository<VwCategoryStatusCount>().ToListAsync(cancellationToken);
			IList<GetCategoryOverviewsQueryResponse> response = mapper.Map<IList<GetCategoryOverviewsQueryResponse>>(vwCategoryStatusCounts);

			var statusCounts = response.ToDictionary(r => r.Status);

			response = Enum.GetValues(typeof(Status))
				.Cast<Status>()
				.Select(status => statusCounts.TryGetValue(status, out var item)
					? item
					: new GetCategoryOverviewsQueryResponse
					{
						Status = status,
						Count = 0
					})
				.ToList();

			return response;
		}
	}
}
