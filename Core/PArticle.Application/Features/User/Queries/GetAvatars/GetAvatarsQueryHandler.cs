using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.User.Queries.GetAvatars
{

	public class GetAvatarsQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.User>(uow, httpContextAccessor, mapper), IRequestHandler<GetAvatarsQueryRequest, IList<GetAvatarsQueryResponse>>
	{
		public async Task<IList<GetAvatarsQueryResponse>> Handle(GetAvatarsQueryRequest request, CancellationToken cancellationToken)
		{
			if (request?.Ids == null)
				return [];
			return await readRepository.GetListAsync(predicate: x => request.Ids.Contains(x.Id), cancellationToken: cancellationToken, select: x => new GetAvatarsQueryResponse
			{
				Id = x.Id,
				Avatar = x.AvatarPath
			});

		}
	}
}
