using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Domain.Views;

namespace PArticle.Application.Features.Stats.Queries.GetVisitors
{
	public class GetVisitorsQueryHandler(IUow uow,IHttpContextAccessor httpContextAccessor,IMapper mapper):BaseHandler(uow,httpContextAccessor,mapper),IRequestHandler<GetVisitorsQueryRequest, IList<GetVisitorsQueryResponse>>
	{
		public async Task<IList<GetVisitorsQueryResponse>> Handle(GetVisitorsQueryRequest request, CancellationToken cancellationToken)
		{
			IList<VwArticleViewDaily> visits= await uow.GetViewRepository<VwArticleViewDaily>().ToListAsync(v => v.ViewDay >= request.StartDate && v.ViewDay <= request.EndDate, cancellationToken: cancellationToken);

			List<GetVisitorsQueryResponse> response = mapper.Map<List<GetVisitorsQueryResponse>>(visits);
			return response;
		}
	}
}
