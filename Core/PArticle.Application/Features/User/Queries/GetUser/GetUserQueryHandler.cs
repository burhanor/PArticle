using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.User.Queries.GetUser
{
	

	public class GetUserQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.User>(uow, httpContextAccessor, mapper), IRequestHandler<GetUserQueryRequest, GetUserQueryResponse?>
	{
		public async Task<GetUserQueryResponse?> Handle(GetUserQueryRequest request, CancellationToken cancellationToken)
		{
			GetUserQueryResponse? response = null;
			Domain.Entities.User? user = await readRepository.FindAsync(request.Id, cancellationToken: cancellationToken);
			if (user != null)
			{
				response = mapper.Map<GetUserQueryResponse>(user);
				return response;
			}
			return response;

		}
	}
}
