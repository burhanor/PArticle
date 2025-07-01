using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Redis;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.Menu.Queries.GetMenuItem
{


	public class GetMenuItemQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService, IRedisService redisService) : BaseHandler<Domain.Entities.MenuItem>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<GetMenuItemQueryRequest, GetMenuItemQueryResponse?>
	{
		public async Task<GetMenuItemQueryResponse?> Handle(GetMenuItemQueryRequest request, CancellationToken cancellationToken)
		{
			GetMenuItemQueryResponse? response = await redisService.GetAsync<GetMenuItemQueryResponse>("menu_items", request.Id.ToString());
			if (response is null)
			{
				Domain.Entities.MenuItem? tag = await readRepository.FindAsync(request.Id, cancellationToken: cancellationToken);
				if (tag != null)
				{
					response = mapper.Map<GetMenuItemQueryResponse>(tag);
					return response;
				}
			}
			return response;

		}
	}
}
