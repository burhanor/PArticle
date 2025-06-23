using AutoMapper;
using Domain.Contracts.Enums;
using Domain.Contracts.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Enums;
using PArticle.Application.Extentions;
using PArticle.Application.Features.Article.Queries.GetArticle;
using PArticle.Application.Helpers;
using PArticle.Application.Helpers.FeatureHelpers;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Article.Commands.CreateArticle
{
	public class CreateArticleCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.Article>(uow, httpContextAccessor, mapper), IRequestHandler<CreateArticleCommandRequest, ResponseContainer<CreateArticleCommandResponse>>
	{
		public async Task<ResponseContainer<CreateArticleCommandResponse>> Handle(CreateArticleCommandRequest request, CancellationToken cancellationToken)
		{

			ResponseContainer<CreateArticleCommandResponse> response = await ValidationHelper.ValidateAsync<CreateArticleCommandRequest, CreateArticleCommandResponse, CreateArticleCommandValidator>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool slugUnique = await readRepository.UniqueAsync(m => m.Slug == request.Slug, cancellationToken);
			if (!slugUnique)
			{
				response.AddValidationError(nameof(request.Slug), Messages.Article.ARTICLE_SLUG_ALREADY_EXIST);
				return response;
			}
			List<int> categoryIds = await ArticleHelper.GetOrCreateEntityIdsAsync<Domain.Entities.Category>(request.Categories, uow, cancellationToken);
			List<int> tagIds = await ArticleHelper.GetOrCreateEntityIdsAsync<Domain.Entities.Tag>(request.Tags, uow, cancellationToken);
			Domain.Entities.Article article = mapper.Map<Domain.Entities.Article>(request);
			if (article.Status == Status.Published)
			{
				article.PublishDate = DateTime.Now;
			}
			
			if (categoryIds.Count > 0)
				article.ArticleCategories = [.. categoryIds.Select(m => new Domain.Entities.ArticleCategory
				{
					CategoryId = m
				})];
			if (tagIds.Count > 0)
				article.ArticleTags = [.. tagIds.Select(m => new Domain.Entities.ArticleTag
				{
					TagId = m
				})];

			article.UserId = userId;
			await writeRepository.AddAsync(article, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (article.Id > 0)
			{
				GetArticleQueryResponse? articleResponse = await ArticleHelper.GetArticle(article.Id,uow,httpContextAccessor,mapper,cancellationToken);
				response.Data = mapper.Map<CreateArticleCommandResponse>(articleResponse);
				response.Message = Messages.Article.ARTICLE_CREATE_SUCCESS;
				response.Status = ResponseStatus.Success;
			}
			else
			{
				response.Message = Messages.Article.ARTICLE_CREATE_FAILED;
				response.Status = ResponseStatus.Failed;
			}
			return response;
		}


	}
}
