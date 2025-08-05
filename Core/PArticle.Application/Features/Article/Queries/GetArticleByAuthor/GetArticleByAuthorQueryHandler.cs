using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.ElasticSearch;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Models;
using PArticle.Application.Models.Article;

namespace PArticle.Application.Features.Article.Queries.GetArticleByAuthor
{
	


	public class GetArticleByAuthorQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService, IElasticSearchService<ArticleModel> elasticSearchService) : BaseHandler<Domain.Entities.Article>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<GetArticleByAuthorQueryRequest, PaginationContainer<GetArticleByAuthorQueryResponse>>
	{

		public async Task<PaginationContainer<GetArticleByAuthorQueryResponse>> Handle(GetArticleByAuthorQueryRequest request, CancellationToken cancellationToken)
		{
			int userId = await uow.GetReadRepository<Domain.Entities.User>().GetAsync(predicate:m => m.Nickname == Uri.UnescapeDataString(request.Nickname), select:m=>m.Id, cancellationToken: cancellationToken);
			if (userId > 0)
			{
				var result = await elasticSearchService.SearchByAuthorAsync(userId, request.PageNumber, request.PageSize);
				PaginationContainer<GetArticleByAuthorQueryResponse> response = new(request.PageNumber, request.PageSize, result.TotalCount)
				{
					Items = mapper.Map<List<GetArticleByAuthorQueryResponse>>(result.Results)
				};
				return response;
			}
			return new PaginationContainer<GetArticleByAuthorQueryResponse>(request.PageNumber, request.PageSize, 0)
			{
				Items = []
			};


		}
	}

}
