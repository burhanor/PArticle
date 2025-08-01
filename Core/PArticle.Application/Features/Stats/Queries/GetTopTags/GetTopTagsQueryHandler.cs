using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.Stats.Queries.GetTopTags
{
	// En cok kullanılan etiketleri listelemek icin
	public class GetTopTagsQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler(uow, httpContextAccessor, mapper), IRequestHandler<GetTopTagsQueryRequest, IList<GetTopTagsQueryResponse>>
	{
		public async Task<IList<GetTopTagsQueryResponse>> Handle(GetTopTagsQueryRequest request, CancellationToken cancellationToken)
		{
			var storedProcedureRepository = uow.GetStoredProcedureRepository();
			var storedProcedureResponse = await storedProcedureRepository.GetTopTags(request.Limit, cancellationToken);

			List<GetTopTagsQueryResponse> response = mapper.Map<List<GetTopTagsQueryResponse>>(storedProcedureResponse);
			return response;
		}

	}
}
