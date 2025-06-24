using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Tag.Queries.GetTags
{
	

	public class GetTagsQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Tag>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<GetTagsQueryRequest, PaginationContainer<GetTagsQueryResponse>>
	{
		public async Task<PaginationContainer<GetTagsQueryResponse>> Handle(GetTagsQueryRequest request, CancellationToken cancellationToken)
		{
			IQueryable<Domain.Entities.Tag> query = readRepository.Query();
			if (!string.IsNullOrEmpty(request.Name))
				query = query.Where(x => x.Name.Contains(request.Name));
			if (!string.IsNullOrEmpty(request.Slug))
				query = query.Where(x => x.Slug.Contains(request.Slug));
			if (request.Status.HasValue)
				query = query.Where(x => x.Status == request.Status.Value);
			int totalCount = await readRepository.CountAsync(query, cancellationToken);
			if (request.PageNumber.HasValue)
				query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.GetValueOrDefault(10)).Take(request.PageSize.GetValueOrDefault(10));
			else if (request.PageSize.HasValue)
				query = query.Take(request.PageSize.Value);

			PaginationContainer<GetTagsQueryResponse> response = new(request.PageNumber, request.PageSize, totalCount)
			{
				Items = mapper.Map<List<GetTagsQueryResponse>>(query)
			};
			return response;


		}
	}

}
