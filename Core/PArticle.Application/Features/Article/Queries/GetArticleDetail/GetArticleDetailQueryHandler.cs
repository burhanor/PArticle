using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Models.Category;
using PArticle.Application.Models.Tag;
using PArticle.Domain.Views;

namespace PArticle.Application.Features.Article.Queries.GetArticleDetail
{
	

	public class GetArticleDetailQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Article>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<GetArticleDetailQueryRequest, GetArticleDetailQueryResponse?>
	{
		public async Task<GetArticleDetailQueryResponse?> Handle(GetArticleDetailQueryRequest request, CancellationToken cancellationToken)
		{
			GetArticleDetailQueryResponse? response = null;
			Domain.Entities.Article? article = await readRepository.GetAsync(m=>m.Slug==request.Slug, cancellationToken: cancellationToken);
			if (article is null)
				return response;

			var articleTags = await uow.GetViewRepository<VwArticleTag>().ToListAsync(m => m.ArticleId == article.Id, cancellationToken);
			var articleCategories = await uow.GetViewRepository<VwArticleCategory>().ToListAsync(m => m.ArticleId == article.Id, cancellationToken);
			response = mapper.Map<GetArticleDetailQueryResponse>(article);
			response.Tags = mapper.Map<List<TagDto>>(articleTags);
			response.Categories = mapper.Map<List<CategoryDto>>(articleCategories);
			response.Nickname = await uow.GetReadRepository<Domain.Entities.User>().GetAsync(predicate: m => m.Id == article.UserId, select: m => m.Nickname, cancellationToken: cancellationToken);

			await uow.GetWriteRepository<Domain.Entities.ArticleView>().AddAsync(new Domain.Entities.ArticleView
			{
				ArticleId = article.Id,
				IpAddress = ipAddress,
				ViewDate = DateTime.UtcNow
			},cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);


			return response;
		}
	}
}
