using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.Stats.Queries.GetTopCategories
{
	

	public class GetTopCategoriesQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler(uow, httpContextAccessor, mapper), IRequestHandler<GetTopCategoriesQueryRequest, IList<GetTopCategoriesQueryResponse>>
	{
		public async Task<IList<GetTopCategoriesQueryResponse>> Handle(GetTopCategoriesQueryRequest request, CancellationToken cancellationToken)
		{
			var storedProcedureRepository = uow.GetStoredProcedureRepository();
			var storedProcedureResponse = await storedProcedureRepository.GetTopCategories(request.Limit, cancellationToken);

			List<GetTopCategoriesQueryResponse> response = mapper.Map<List<GetTopCategoriesQueryResponse>>(storedProcedureResponse);
			return response;
		}

	}
}
