using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Redis;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.Category.Queries.GetCategory
{
	public class GetCategoryQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService,IRedisService redisService) : BaseHandler<Domain.Entities.Category>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<GetCategoryQueryRequest, GetCategoryQueryResponse?>
	{
		public async Task<GetCategoryQueryResponse?> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
		{
			GetCategoryQueryResponse? response = await redisService.GetAsync<GetCategoryQueryResponse>("categories", request.Id.ToString());
			if (response is null)
			{
				Domain.Entities.Category? category = await readRepository.FindAsync(request.Id, cancellationToken: cancellationToken);
				if (category != null)
				{
					response = mapper.Map<GetCategoryQueryResponse>(category);
					return response;
				}
			}
			return response;

		}
	}
}
