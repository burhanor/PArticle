using MediatR;
using Microsoft.AspNetCore.Mvc;
using Particle.API.Extensions;
using PArticle.Application.Features.Stats.Queries.GetArticleOverviews;
using PArticle.Application.Features.Stats.Queries.GetArticleRates;
using PArticle.Application.Features.Stats.Queries.GetCategoryOverviews;
using PArticle.Application.Features.Stats.Queries.GetTagOverviews;
using PArticle.Application.Features.Stats.Queries.GetTopArticles;
using PArticle.Application.Features.Stats.Queries.GetTopAuthors;
using PArticle.Application.Features.Stats.Queries.GetTopCategories;
using PArticle.Application.Features.Stats.Queries.GetTopTags;
using PArticle.Application.Features.Stats.Queries.GetUserOverviews;
using PArticle.Application.Models.Stats;

namespace Particle.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StatsController(IMediator mediator) : ControllerBase
	{

		[HttpGet("article-status")]
		public async Task<IActionResult> GetArticleStatusCount()
		{
			GetArticleOverviewsQueryRequest request = new();
			return await this.GetAsync(mediator, request);
		}
		[HttpGet("category-status")]
		public async Task<IActionResult> GetCategoryStatusCount()
		{
			GetCategoryOverviewsQueryRequest request = new();
			return await this.GetAsync(mediator, request);
		}
		[HttpGet("tag-status")]
		public async Task<IActionResult> GetTagStatusCount()
		{
			GetTagOverviewsQueryRequest request = new();
			return await this.GetAsync(mediator, request);
		}

		[HttpGet("user-types")]
		public async Task<IActionResult> GetUserTypesCount()
		{
			GetUserOverviewsQueryRequest request = new();
			return await this.GetAsync(mediator, request);
		}

		[HttpGet("top-tags")]
		public async Task<IActionResult> GetTopTags(int limit=5)
		{
			GetTopTagsQueryRequest request = new(limit);
			return await this.GetAsync(mediator, request);
		}
		[HttpGet("top-categories")]
		public async Task<IActionResult> GetTopCategories(int limit = 5)
		{
			GetTopCategoriesQueryRequest request = new(limit);
			return await this.GetAsync(mediator, request);
		}

		[HttpGet("top-authors")]
		public async Task<IActionResult> GetTopAuthors(int limit = 5)
		{
			GetTopAuthorsQueryRequest request = new(limit);
			return await this.GetAsync(mediator, request);
		}

		[HttpGet("top-articles")]
		public async Task<IActionResult> GetTopArticles([FromQuery] DateLimitModel model)
		{
			GetTopArticlesQueryRequest request = new(model.StartDate,model.EndDate,model.Count);
			return await this.GetAsync(mediator, request);
		}
		[HttpGet("article-rates")]
		public async Task<IActionResult> GetArticleRates([FromQuery] DateLimitModel model)
		{
			GetArticleRatesQueryRequest request = new(model.StartDate, model.EndDate, model.Count);
			return await this.GetAsync(mediator, request);
		}
	}
}
