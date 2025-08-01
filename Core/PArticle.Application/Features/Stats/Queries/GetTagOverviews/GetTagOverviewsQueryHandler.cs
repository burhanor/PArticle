using AutoMapper;
using Domain.Contracts.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Domain.Views;

namespace PArticle.Application.Features.Stats.Queries.GetTagOverviews
{
	

	public class GetTagOverviewsQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.Tag>(uow, httpContextAccessor, mapper), IRequestHandler<GetTagOverviewsQueryRequest, IList<GetTagOverviewsQueryResponse>>
	{
		public async Task<IList<GetTagOverviewsQueryResponse>> Handle(GetTagOverviewsQueryRequest request, CancellationToken cancellationToken)
		{
			IList<VwTagStatusCount> vwTagStatusCounts = await uow.GetViewRepository<VwTagStatusCount>().ToListAsync(cancellationToken);
			IList<GetTagOverviewsQueryResponse> response = mapper.Map<IList<GetTagOverviewsQueryResponse>>(vwTagStatusCounts);

			var statusCounts = response.ToDictionary(r => r.Status);

			response = Enum.GetValues(typeof(Status))
				.Cast<Status>()
				.Select(status => statusCounts.TryGetValue(status, out var item)
					? item
					: new GetTagOverviewsQueryResponse
					{
						Status = status,
						Count = 0
					})
				.ToList();

			return response;
		}
	}

}
