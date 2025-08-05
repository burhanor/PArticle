using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.User.Queries.NicknameIsExist
{


	public class NicknameIsExistQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.User>(uow, httpContextAccessor, mapper), IRequestHandler<NicknameIsExistQueryRequest, bool>
	{
		public async Task<bool> Handle(NicknameIsExistQueryRequest request, CancellationToken cancellationToken)
		{
			return await readRepository.ExistAsync(x => x.Nickname == request.Nickname , cancellationToken);
		}
	}
}
