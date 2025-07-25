using AutoMapper;
using Domain.Contracts.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Particle.API.Extensions;
using PArticle.Application.Features.Article.Commands.CreateArticle;
using PArticle.Application.Features.Article.Commands.DeleteArticle;
using PArticle.Application.Features.Article.Commands.InsertArticleView;
using PArticle.Application.Features.Article.Commands.ResetArticleView;
using PArticle.Application.Features.Article.Commands.ResetArticleVotes;
using PArticle.Application.Features.Article.Commands.UpdateArticle;
using PArticle.Application.Features.Article.Commands.UpsertArticleVote;
using PArticle.Application.Features.Article.Queries.GetArticle;
using PArticle.Application.Features.Article.Queries.GetArticles;
using PArticle.Application.Features.Article.Queries.GetArticleViewCount;
using PArticle.Application.Features.Article.Queries.GetArticleVotes;
using PArticle.Application.Features.Article.Queries.GetMostViewedArticles;
using PArticle.Application.Features.Article.Queries.GetTopRatedArticles;
using PArticle.Application.Models.Article;

namespace Particle.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArticleController(IMediator mediator, IMapper mapper) : ControllerBase
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
		[HttpDelete]
		[Authorize]
		public async Task<IActionResult> DeleteArticles([FromBody] List<int> ids)
		{
			DeleteArticleCommandRequest request = new(ids);
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
		#region Views

		[HttpGet("{id}/views")]
		public async Task<IActionResult> GetViewCount(int id)
		{
			GetArticleViewCountQueryRequest request = new(id);
			return await this.GetByIdAsync(mediator, request);
		}

		[HttpPost("{id}/views")]
		[Authorize]
		public async Task<IActionResult> AddView(int id)
		{
			InsertArticleViewCommandRequest request = new(id);
			return await this.CreateAsync(mediator, request);
		}

		[HttpDelete("{id}/views")]
		public async Task<IActionResult> ResetViewCount(int id)
		{
			ResetArticleViewCommandRequest request = new(id);
			return await this.DeleteAsync(mediator, request);
		}
		#endregion

		#region Votes
		[HttpGet("{id}/votes")]
		public async Task<IActionResult> GetVotes(int id)
		{
			GetArticleVotesQueryRequest request = new(id);
			return await this.GetByIdAsync(mediator, request);
		}

		[HttpGet("{id}/votes/{voteType}")]
		public async Task<IActionResult> GetVotesByVoteType(int id,int voteType)
		{
			GetArticleVotesQueryRequest request = new(id, (ArticleVote?)voteType);
			return await this.GetByIdAsync(mediator, request);
		}

		[HttpPost("{id}/votes")]
		[Authorize]
		public async Task<IActionResult> AddVote(int id, [FromBody] ArticleVoteModel model)
		{
			UpsertArticleVoteCommandRequest request = new(id,model.ArticleVote);
			return await this.CreateAsync(mediator, request);
		}
		[HttpDelete("{id}/votes")]
		public async Task<IActionResult> ResetVotes(int id)
		{
			ResetArticleVotesCommandRequest request = new(id);
			return await this.DeleteAsync(mediator, request);
		}
		[HttpDelete("{id}/votes/{voteType}")]
		public async Task<IActionResult> ResetVotes(int id,int voteType)
		{
			ResetArticleVotesCommandRequest request = new(id,(ArticleVote?)voteType);
			return await this.DeleteAsync(mediator, request);
		}
		#endregion

		[HttpGet("most-viewed")]
		public async Task<IActionResult> GetMostViewedArticles(int count)
		{
			GetMostViewedArticlesQueryRequest request = new(count);
			return await this.GetAsync(mediator, request);
		}

		[HttpGet("top-rated")]
		public async Task<IActionResult> GetTopRatedArticles(int count)
		{
			GetTopRatedArticlesQueryRequest request = new(count);
			return await this.GetAsync(mediator, request);
		}

	}
}
