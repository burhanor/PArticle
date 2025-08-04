using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.ElasticSearch;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Models;
using PArticle.Application.Models.Article;

namespace PArticle.Application.Features.Article.Queries.GetArticleByCategory
{


	public class GetArticleByCategoryQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService, IElasticSearchService<ArticleModel> elasticSearchService) : BaseHandler<Domain.Entities.Article>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<GetArticleByCategoryQueryRequest, PaginationContainer<GetArticleByCategoryQueryResponse>>
	{
	
		public async Task<PaginationContainer<GetArticleByCategoryQueryResponse>> Handle(GetArticleByCategoryQueryRequest request, CancellationToken cancellationToken)
		{
			var result = await elasticSearchService.SearchByCategoryAsync(request.CategorySlug ?? string.Empty, request.PageNumber, request.PageSize);

			PaginationContainer<GetArticleByCategoryQueryResponse> response = new(request.PageNumber, request.PageSize, result.TotalCount)
			{
				Items = mapper.Map<List<GetArticleByCategoryQueryResponse>>(result.Results)
			};
			return response;

		}
	}
}
