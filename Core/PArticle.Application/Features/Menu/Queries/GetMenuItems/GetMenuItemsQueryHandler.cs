using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Menu.Queries.GetMenuItems
{
	

	public class GetMenuItemsQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.MenuItem>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<GetMenuItemsQueryRequest, PaginationContainer<GetMenuItemsQueryResponse>>
	{
		public async Task<PaginationContainer<GetMenuItemsQueryResponse>> Handle(GetMenuItemsQueryRequest request, CancellationToken cancellationToken)
		{
			IQueryable<Domain.Entities.MenuItem> query = readRepository.Query();
			if (!string.IsNullOrEmpty(request.Title))
				query = query.Where(x => x.Title.Contains(request.Title));
			if (!string.IsNullOrEmpty(request.Description))
				query = query.Where(x => x.Description.Contains(request.Description));
			if (!string.IsNullOrEmpty(request.Link))
				query = query.Where(x => x.Link.Contains(request.Link));
			if (request.MenuType.HasValue)
				query = query.Where(x => x.MenuType == request.MenuType.Value);
			int totalCount = await readRepository.CountAsync(query, cancellationToken);
			if (request.PageNumber.HasValue)
				query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.GetValueOrDefault(10)).Take(request.PageSize.GetValueOrDefault(10));
			else if (request.PageSize.HasValue)
				query = query.Take(request.PageSize.Value);

			PaginationContainer<GetMenuItemsQueryResponse> response = new(request.PageNumber, request.PageSize, totalCount)
			{
				Items = mapper.Map<List<GetMenuItemsQueryResponse>>(query)
			};
			return response;


		}
	}

}
