using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Particle.API.Extensions;
using PArticle.Application.Features.Tag.Commands.CreateTag;
using PArticle.Application.Features.Tag.Commands.DeleteTag;
using PArticle.Application.Features.Tag.Commands.UpdateTag;
using PArticle.Application.Features.Tag.Queries.GetTag;
using PArticle.Application.Features.Tag.Queries.GetTags;
using PArticle.Application.Models.Tag;

namespace Particle.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	


	public class TagController(IMapper mapper, IMediator mediator) : ControllerBase
	{

		[HttpGet]
		public async Task<IActionResult> GetAllTags([FromQuery] TagFilterModel model)
		{
			GetTagsQueryRequest request = mapper.Map<GetTagsQueryRequest>(model);
			return await this.GetAsync(mediator, request);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetTagById(int id)
		{
			GetTagQueryRequest request = new(id);
			return await this.GetByIdAsync(mediator, request);
		}
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateTag([FromBody] TagDto model)
		{
			CreateTagCommandRequest request = mapper.Map<CreateTagCommandRequest>(model);
			return await this.CreateAsync(mediator, request);
		}
		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> UpdateTag(int id, [FromBody] TagDto model)
		{
			UpdateTagCommandRequest request = mapper.Map<UpdateTagCommandRequest>(model);
			request.Id = id;
			return await this.UpdateAsync(mediator, request);
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> DeleteTag(int id)
		{
			DeleteTagCommandRequest request = new(id);
			return await this.DeleteAsync(mediator, request);
		}
		[HttpDelete]
		[Authorize]
		public async Task<IActionResult> DeleteTags([FromBody] List<int> ids)
		{
			DeleteTagCommandRequest request = new(ids);
			return await this.DeleteAsync(mediator, request);
		}


	}

}
