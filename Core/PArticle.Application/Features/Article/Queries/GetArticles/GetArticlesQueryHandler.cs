using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.ElasticSearch;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Models;
using PArticle.Application.Models.Article;

namespace PArticle.Application.Features.Article.Queries.GetArticles
{
	public class GetArticlesQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService,IElasticSearchService<ArticleModel> elasticSearchService) : BaseHandler<Domain.Entities.Article>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<GetArticlesQueryRequest, PaginationContainer<GetArticlesQueryResponse>>
	{
		public async Task<PaginationContainer<GetArticlesQueryResponse>> Handle(GetArticlesQueryRequest request, CancellationToken cancellationToken)
		{
			var statusString = request.Status?.ToString() ?? string.Empty;
			var result = string.IsNullOrEmpty(statusString)
				? await elasticSearchService.SearchAsync(request.SearchKey ?? string.Empty, request.PageNumber, request.PageSize)
				: await elasticSearchService.SearchAsync(statusString, request.SearchKey ?? string.Empty, request.PageNumber, request.PageSize);

			PaginationContainer<GetArticlesQueryResponse> response = new(request.PageNumber, request.PageSize, result.TotalCount)
			{
				Items = mapper.Map<List<GetArticlesQueryResponse>>(result.Results)
			};
			return response;
		}
	}
}
