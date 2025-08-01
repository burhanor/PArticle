using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.Stats.Queries.GetTopAuthors
{
	

	public class GetTopAuthorsQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler(uow, httpContextAccessor, mapper), IRequestHandler<GetTopAuthorsQueryRequest, IList<GetTopAuthorsQueryResponse>>
	{
		public async Task<IList<GetTopAuthorsQueryResponse>> Handle(GetTopAuthorsQueryRequest request, CancellationToken cancellationToken)
		{
			var storedProcedureRepository = uow.GetStoredProcedureRepository();
			var storedProcedureResponse = await storedProcedureRepository.GetTopAuthors(request.Limit, cancellationToken);

			List<GetTopAuthorsQueryResponse> response = mapper.Map<List<GetTopAuthorsQueryResponse>>(storedProcedureResponse);
			return response;
		}

	}
}
