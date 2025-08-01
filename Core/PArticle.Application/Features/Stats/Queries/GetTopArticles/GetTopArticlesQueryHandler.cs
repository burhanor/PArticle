using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.Stats.Queries.GetTopArticles
{
	public class GetTopArticlesQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler(uow, httpContextAccessor, mapper), IRequestHandler<GetTopArticlesQueryRequest, IList<GetTopArticlesQueryResponse>>
	{
		public async Task<IList<GetTopArticlesQueryResponse>> Handle(GetTopArticlesQueryRequest request, CancellationToken cancellationToken)
		{
			var storedProcedureRepository = uow.GetStoredProcedureRepository();
			var storedProcedureResponse = await storedProcedureRepository.GetTopArticles(request.StartDate,request.EndDate, request.Count, cancellationToken);
			List<GetTopArticlesQueryResponse> response = mapper.Map<List<GetTopArticlesQueryResponse>>(storedProcedureResponse);
			return response;
		}
	}
}
