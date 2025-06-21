using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Particle.API.Extensions;
using PArticle.Application.Features.Category.Commands.CreateCategory;
using PArticle.Application.Features.Category.Commands.DeleteCategory;
using PArticle.Application.Features.Category.Commands.UpdateCategory;
using PArticle.Application.Features.Category.Queries.GetCategories;
using PArticle.Application.Features.Category.Queries.GetCategory;
using PArticle.Application.Models.Category;
using System.Threading.Tasks;

namespace Particle.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController(IMapper mapper,IMediator mediator) : ControllerBase
	{

		[HttpGet]
		public async Task<IActionResult> GetAllCategories([FromQuery] CategoryFilterModel model)
		{
			GetCategoriesQueryRequest request = mapper.Map<GetCategoriesQueryRequest>(model);
			return await this.GetAsync(mediator, request);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			GetCategoryQueryRequest request = new(id);
			return await this.GetByIdAsync(mediator, request);
		}
		[HttpPost]
		public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateModel model)
		{
			CreateCategoryCommandRequest request = mapper.Map<CreateCategoryCommandRequest>(model);
			return await this.CreateAsync(mediator,request);
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateModel model)
		{
			UpdateCategoryCommandRequest request = mapper.Map<UpdateCategoryCommandRequest>(model);
			request.Id = id;
			return await this.UpdateAsync(mediator,request);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			DeleteCategoryCommandRequest request = new (id);
			return await this.DeleteAsync(mediator, request);
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteCategories([FromBody]  List<int> ids)
		{
			DeleteCategoryCommandRequest request = new(ids);
			return await this.DeleteAsync(mediator, request);
		}
		

	}
}
