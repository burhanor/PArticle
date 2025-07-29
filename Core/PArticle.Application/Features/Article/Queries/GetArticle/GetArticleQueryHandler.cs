using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Models.Category;
using PArticle.Application.Models.Tag;
using PArticle.Domain.Views;

namespace PArticle.Application.Features.Article.Queries.GetArticle
{
	public class GetArticleQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Article>(uow,httpContextAccessor,mapper, rabbitMqService), IRequestHandler<GetArticleQueryRequest, GetArticleQueryResponse?>
	{
		public async Task<GetArticleQueryResponse?> Handle(GetArticleQueryRequest request, CancellationToken cancellationToken)
		{
			GetArticleQueryResponse? response=null;
			Domain.Entities.Article? article = await readRepository.FindAsync(request.Id, cancellationToken: cancellationToken);
			if (article is null)
				return response;

			var articleTags = await uow.GetViewRepository<VwArticleTag>().ToListAsync(m => m.ArticleId == request.Id, cancellationToken);
			var articleCategories = await uow.GetViewRepository<VwArticleCategory>().ToListAsync(m => m.ArticleId == request.Id, cancellationToken);
			response = mapper.Map<GetArticleQueryResponse>(article);
			response.Tags= mapper.Map<List<TagDto>>(articleTags);
			response.Categories = mapper.Map<List<CategoryDto>>(articleCategories);
			response.Nickname = await uow.GetReadRepository<Domain.Entities.User>().GetAsync(predicate: m => m.Id == article.UserId, select: m => m.Nickname, cancellationToken: cancellationToken);

			return response;
		}
	}
}
