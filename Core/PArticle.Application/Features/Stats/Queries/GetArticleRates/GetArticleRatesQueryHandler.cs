using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Features.Stats.Queries.GetTopArticles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Stats.Queries.GetArticleRates
{
	

	public class GetArticleRatesQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler(uow, httpContextAccessor, mapper), IRequestHandler<GetArticleRatesQueryRequest, IList<GetArticleRatesQueryResponse>>
	{
		public async Task<IList<GetArticleRatesQueryResponse>> Handle(GetArticleRatesQueryRequest request, CancellationToken cancellationToken)
		{
			var storedProcedureRepository = uow.GetStoredProcedureRepository();
			var storedProcedureResponse = await storedProcedureRepository.GetArticleRates(request.StartDate, request.EndDate, request.Count, cancellationToken);
			List<GetArticleRatesQueryResponse> response = mapper.Map<List<GetArticleRatesQueryResponse>>(storedProcedureResponse);
			return response;
		}
	}
}
