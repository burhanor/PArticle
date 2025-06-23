using AutoMapper;
using Domain.Contracts.Enums;
using Domain.Contracts.Interfaces;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Extentions;
using PArticle.Application.Features.Article.Queries.GetArticle;

namespace PArticle.Application.Helpers.FeatureHelpers
{
	public static class ArticleHelper
	{
		public static async Task<List<int>> GetOrCreateEntityIdsAsync<TEntity>(List<string>? names, IUow uow, CancellationToken cancellationToken) where TEntity : class, IEntityWithNameSlug, new()
		{
			var ids = new List<int>();
			if (names == null || names.Count == 0)
				return ids;

			var distinctNames = names.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
			var readRepo = uow.GetReadRepository<TEntity>();
			var writeRepo = uow.GetWriteRepository<TEntity>();

			foreach (var name in distinctNames)
			{
				var existingId = readRepo.Query()
					.Where(e => e.Name == name)
					.Select(e => e.Id)
					.FirstOrDefault();

				if (existingId > 0)
				{
					ids.Add(existingId);
					continue;
				}

				var slug = name.ToSlug();
				if (await readRepo.ExistAsync(e => e.Slug == slug, cancellationToken))
				{
					slug = Guid.NewGuid().ToString();
				}

				var entity = new TEntity
				{
					Name = name,
					Slug = slug,
					Status = Status.Pending
				};

				await writeRepo.AddAsync(entity, cancellationToken);
				await uow.SaveChangesAsync(cancellationToken);
				ids.Add(entity.Id);
			}

			return ids;
		}


		public static async Task<GetArticleQueryResponse?> GetArticle(int articleId,IUow uow,IHttpContextAccessor httpContextAccessor,IMapper mapper,CancellationToken cancellationToken)
		{
			GetArticleQueryRequest getArticleQueryRequest = new(articleId);
			GetArticleQueryHandler getArticleQueryHandler = new(uow, httpContextAccessor, mapper);
			return await getArticleQueryHandler.Handle(getArticleQueryRequest, cancellationToken);
		}
	}
}
