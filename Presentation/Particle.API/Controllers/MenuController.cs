using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Particle.API.Extensions;
using PArticle.Application.Features.Menu.Commands.CreateMenuItem;
using PArticle.Application.Features.Menu.Commands.DeleteMenuItem;
using PArticle.Application.Features.Menu.Commands.UpdateMenuItem;
using PArticle.Application.Features.Menu.Queries.GetMenuItem;
using PArticle.Application.Features.Menu.Queries.GetMenuItems;
using PArticle.Application.Models.Menu;

namespace Particle.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MenuController(IMapper mapper, IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAllMenus([FromQuery] MenuFilterModel model)
		{
			GetMenuItemsQueryRequest request = mapper.Map<GetMenuItemsQueryRequest>(model);
			return await this.GetAsync(mediator, request);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetMenuById(int id)
		{
			GetMenuItemQueryRequest request = new(id);
			return await this.GetByIdAsync(mediator, request);
		}
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateMenu([FromBody] MenuDto model)
		{
			CreateMenuItemCommandRequest request = mapper.Map<CreateMenuItemCommandRequest>(model);
			return await this.CreateAsync(mediator, request);
		}
		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> UpdateMenu(int id, [FromBody] MenuDto model)
		{
			UpdateMenuItemCommandRequest request = mapper.Map<UpdateMenuItemCommandRequest>(model);
			request.Id = id;
			return await this.UpdateAsync(mediator, request);
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> DeleteMenu(int id)
		{
			DeleteMenuItemCommandRequest request = new(id);
			return await this.DeleteAsync(mediator, request);
		}
		[HttpDelete]
		[Authorize]
		public async Task<IActionResult> DeleteMenus([FromBody] List<int> ids)
		{
			DeleteMenuItemCommandRequest request = new(ids);
			return await this.DeleteAsync(mediator, request);
		}
	}

}
