using AutoMapper;
using Domain.Contracts.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Enums;
using PArticle.Application.Features.Article.Queries.GetArticle;
using PArticle.Application.Helpers;
using PArticle.Application.Helpers.FeatureHelpers;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Article.Commands.UpdateArticle
{
	public class UpdateArticleCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.Article>(uow, httpContextAccessor, mapper), IRequestHandler<UpdateArticleCommandRequest, ResponseContainer<UpdateArticleCommandResponse>>
	{
		public async Task<ResponseContainer<UpdateArticleCommandResponse>> Handle(UpdateArticleCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<UpdateArticleCommandResponse> response = await ValidationHelper.ValidateAsync<UpdateArticleCommandRequest, UpdateArticleCommandResponse, UpdateArticleCommandValidator>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;
			Domain.Entities.Article? oldArticle = await readRepository.FindAsync(request.Id, cancellationToken: cancellationToken);
			if (oldArticle==null)
			{
				response.Message = Messages.Article.ARTICLE_NOT_FOUND;
				return response;
			}
			bool slugUnique = await readRepository.UniqueAsync(m => m.Slug == request.Slug && m.Id != request.Id, cancellationToken);
			if (!slugUnique)
			{
				response.AddValidationError(nameof(request.Slug), Messages.Article.ARTICLE_SLUG_ALREADY_EXIST);
				return response;
			}
			Domain.Entities.Article article = mapper.Map<Domain.Entities.Article>(request);
			article.UserId = oldArticle.UserId;
			article.PublishDate = oldArticle.PublishDate;
			if (article.Status== Status.Published && article.PublishDate is null)
			{
				article.PublishDate = DateTime.Now;
			}

			var articleCategoryWriteRepository = uow.GetWriteRepository<Domain.Entities.ArticleCategory>();
			var articleTagWriteRepository = uow.GetWriteRepository<Domain.Entities.ArticleTag>();
			articleCategoryWriteRepository.Delete(m => m.ArticleId == article.Id);
			articleTagWriteRepository.Delete(m => m.ArticleId == article.Id);

			List<int> categoryIds = await ArticleHelper.GetOrCreateEntityIdsAsync<Domain.Entities.Category>(request.Categories, uow, cancellationToken);
			List<int> tagIds = await ArticleHelper.GetOrCreateEntityIdsAsync<Domain.Entities.Tag>(request.Tags, uow, cancellationToken);

			if (categoryIds.Count > 0)
				await articleCategoryWriteRepository.AddRangeAsync(categoryIds.Select(m => new Domain.Entities.ArticleCategory
				{
					ArticleId = request.Id,
					CategoryId = m
				}), cancellationToken);
			if (tagIds.Count > 0)
				await articleTagWriteRepository.AddRangeAsync(tagIds.Select(m => new Domain.Entities.ArticleTag
				{
					ArticleId = request.Id,
					TagId = m
				}), cancellationToken);


			if (!writeRepository.Update(article))
			{
				response.Message = Messages.Article.ARTICLE_NOT_FOUND;
				return response;
			}

			GetArticleQueryResponse? articleResponse = await ArticleHelper.GetArticle(article.Id, uow, httpContextAccessor, mapper, cancellationToken);
			response.Data = mapper.Map<UpdateArticleCommandResponse>(articleResponse);
			response.Message = Messages.Article.ARTICLE_UPDATE_SUCCESS;
			response.Status = ResponseStatus.Success;


			await uow.SaveChangesAsync(cancellationToken);
			return response;
		}
	}
}
