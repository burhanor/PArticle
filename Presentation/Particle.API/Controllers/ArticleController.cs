using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Particle.API.Extensions;
using PArticle.Application.Features.Article.Commands.CreateArticle;
using PArticle.Application.Features.Article.Commands.DeleteArticle;
using PArticle.Application.Features.Article.Commands.UpdateArticle;
using PArticle.Application.Features.Article.Queries.GetArticle;
using PArticle.Application.Features.Article.Queries.GetArticles;
using PArticle.Application.Features.Category.Queries.GetCategories;
using PArticle.Application.Models.Article;

namespace Particle.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArticleController(IMediator mediator,IMapper mapper) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAllArticles([FromQuery] ArticleFilterModel model)
		{
			GetArticlesQueryRequest request = mapper.Map<GetArticlesQueryRequest>(model);
			return await this.GetAsync(mediator, request);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetArticleById(int id)
		{
			GetArticleQueryRequest request = new(id);
			return await this.GetByIdAsync(mediator, request);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateArticle([FromBody] ArticleCreateModel model)
		{
			CreateArticleCommandRequest request = mapper.Map<CreateArticleCommandRequest>(model);
			return await this.CreateAsync(mediator, request);
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> DeleteArticle(int id)
		{
			DeleteArticleCommandRequest request = new(id);
			return await this.DeleteAsync(mediator, request);
		}

		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> UpdateArticle(int id, [FromBody] ArticleUpdateModel model)
		{
			UpdateArticleCommandRequest request = mapper.Map<UpdateArticleCommandRequest>(model);
			request.Id = id;
			return await this.UpdateAsync(mediator, request);
		}
	}
}
