using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.User.Queries.GetNicknames
{
	public class GetNicknamesQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.User>(uow, httpContextAccessor, mapper), IRequestHandler<GetNicknamesQueryRequest, IList<GetNicknamesQueryResponse>>
	{
		public async Task<IList<GetNicknamesQueryResponse>> Handle(GetNicknamesQueryRequest request, CancellationToken cancellationToken)
		{
			if(request?.Ids ==null)
				return [];
			return await readRepository.GetListAsync(predicate:x => request.Ids.Contains(x.Id), cancellationToken: cancellationToken, select: x => new GetNicknamesQueryResponse
			 {
				 Id = x.Id,
				 Nickname = x.Nickname
			 });

		}
	}
}
