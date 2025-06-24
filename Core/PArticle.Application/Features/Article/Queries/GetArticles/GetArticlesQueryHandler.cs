using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Features.Category.Queries.GetCategories;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Article.Queries.GetArticles
{
	public class GetArticlesQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Article>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<GetArticlesQueryRequest, PaginationContainer<GetArticlesQueryResponse>>
	{
		public async Task<PaginationContainer<GetArticlesQueryResponse>> Handle(GetArticlesQueryRequest request, CancellationToken cancellationToken)
		{
			//todo: burası tamamen değişecek elasticsearch üzerinden filtreleme yapılacak
			IQueryable<Domain.Entities.Article> query = readRepository.Query();
			if (!string.IsNullOrEmpty(request.Title))
				query = query.Where(x => x.Title.Contains(request.Title));
			if (!string.IsNullOrEmpty(request.Slug))
				query = query.Where(x => x.Slug.Contains(request.Slug));
			if (request.Status.HasValue)
				query = query.Where(x => x.Status == request.Status.Value);
			if(request.PublishMinDate.HasValue)
				query = query.Where(x => x.PublishDate >= request.PublishMinDate.Value);
			if (request.PublishMaxDate.HasValue)
				query = query.Where(x => x.PublishDate <= request.PublishMaxDate.Value);
			if (request.UserId.HasValue)
				query = query.Where(x => x.UserId == request.UserId.Value);
			if (request.CategoryIds?.Count>0)
				query = query.Where(x => x.ArticleCategories.Any(ac => request.CategoryIds.Contains(ac.CategoryId)));
			if (request.TagIds?.Count > 0)
				query = query.Where(x => x.ArticleTags.Any(at => request.TagIds.Contains(at.TagId)));

			int totalCount = await readRepository.CountAsync(query, cancellationToken);
			if (request.PageNumber.HasValue)
				query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.GetValueOrDefault(10)).Take(request.PageSize.GetValueOrDefault(10));
			else if (request.PageSize.HasValue)
				query = query.Take(request.PageSize.Value);

			PaginationContainer<GetArticlesQueryResponse> response = new(request.PageNumber, request.PageSize, totalCount)
			{
				Items = mapper.Map<List<GetArticlesQueryResponse>>(query)
			};
			return response;

		}
	}
}
