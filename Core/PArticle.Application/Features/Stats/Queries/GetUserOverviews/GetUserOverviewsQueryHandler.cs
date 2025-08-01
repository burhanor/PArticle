using AutoMapper;
using Domain.Contracts.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Domain.Views;

namespace PArticle.Application.Features.Stats.Queries.GetUserOverviews
{
	

	public class GetUserOverviewsQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.User>(uow, httpContextAccessor, mapper), IRequestHandler<GetUserOverviewsQueryRequest, IList<GetUserOverviewsQueryResponse>>
	{
		public async Task<IList<GetUserOverviewsQueryResponse>> Handle(GetUserOverviewsQueryRequest request, CancellationToken cancellationToken)
		{
			IList<VwUserTypeCount> vwUserTypeCounts = await uow.GetViewRepository<VwUserTypeCount>().ToListAsync(cancellationToken);
			IList<GetUserOverviewsQueryResponse> response = mapper.Map<IList<GetUserOverviewsQueryResponse>>(vwUserTypeCounts);

			var userTypes = Enum.GetValues(typeof(UserType)).Cast<UserType>().ToList();
			var isActiveValues = new[] { true, false };

			foreach (var userType in userTypes)
			{
				foreach (var isActive in isActiveValues)
				{
					if (!response.Any(r => r.UserType == userType && r.IsActive == isActive))
					{
						response.Add(new GetUserOverviewsQueryResponse
						{
							UserType = userType,
							IsActive = isActive,
							Count = 0
						});
					}
				}
			}

			return response;
		}
	}
}
