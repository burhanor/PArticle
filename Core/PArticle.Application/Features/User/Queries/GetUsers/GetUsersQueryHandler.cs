using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Models;

namespace PArticle.Application.Features.User.Queries.GetUsers
{
	

	public class GetUsersQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.User>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<GetUsersQueryRequest, PaginationContainer<GetUsersQueryResponse>>
	{
		public async Task<PaginationContainer<GetUsersQueryResponse>> Handle(GetUsersQueryRequest request, CancellationToken cancellationToken)
		{
			IQueryable<Domain.Entities.User> query = readRepository.Query();
			if (!string.IsNullOrEmpty(request.Nickname))
				query = query.Where(x => x.Nickname.Contains(request.Nickname));
			if (!string.IsNullOrEmpty(request.EmailAddress))
				query = query.Where(x => x.EmailAddress.Contains(request.EmailAddress));
			if (request.UserType.HasValue)
				query = query.Where(x => x.UserType == request.UserType.Value);
			if (request.IsActive.HasValue)
				query = query.Where(x => x.IsActive == request.IsActive.Value);

			int totalCount = await readRepository.CountAsync(query, cancellationToken);
			if (request.PageNumber.HasValue)
				query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.GetValueOrDefault(10)).Take(request.PageSize.GetValueOrDefault(10));
			else if (request.PageSize.HasValue)
				query = query.Take(request.PageSize.Value);

			PaginationContainer<GetUsersQueryResponse> response = new(request.PageNumber, request.PageSize, totalCount)
			{
				Items = mapper.Map<List<GetUsersQueryResponse>>(query)
			};
			return response;


		}
	}
}
