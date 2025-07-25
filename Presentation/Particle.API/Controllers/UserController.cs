using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Particle.API.Extensions;
using Particle.API.Models;
using PArticle.Application.Features.User.Commands.CreateUser;
using PArticle.Application.Features.User.Commands.DeleteUser;
using PArticle.Application.Features.User.Commands.UpdateUser;
using PArticle.Application.Features.User.Queries.GetNicknames;
using PArticle.Application.Features.User.Queries.GetUser;
using PArticle.Application.Features.User.Queries.GetUsers;
using PArticle.Application.Models.User;

namespace Particle.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController(IMapper mapper, IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAllUsers([FromQuery] UserFilterModel model)
		{
			GetUsersQueryRequest request = mapper.Map<GetUsersQueryRequest>(model);
			return await this.GetAsync(mediator, request);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById(int id)
		{
			GetUserQueryRequest request = new(id);
			return await this.GetByIdAsync(mediator, request);
		}

		[HttpPost("nicknames")]
		public async Task<IActionResult> GetNicknamesByIds([FromBody] List<int> ids)
		{
			GetNicknamesQueryRequest request = new(ids);
			return await this.GetAsync(mediator, request);
		}



		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateUser([FromForm] AppUserModel model)
		{
			CreateUserCommandRequest request = mapper.Map<CreateUserCommandRequest>(model.ToUserRequestModel());
			return await this.CreateAsync(mediator, request);
		}
		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> UpdateUser(int id, [FromForm] AppUserModel model)
		{
			UpdateUserCommandRequest request = mapper.Map<UpdateUserCommandRequest>(model.ToUserRequestModel());
			request.Id = id;
			return await this.UpdateAsync(mediator, request);
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> DeleteUser(int id)
		{
			DeleteUserCommandRequest request = new(id);
			return await this.DeleteAsync(mediator, request);
		}
		[HttpDelete]
		[Authorize]
		public async Task<IActionResult> DeleteUsers([FromBody] List<int> ids)
		{
			DeleteUserCommandRequest request = new(ids);
			return await this.DeleteAsync(mediator, request);
		}
	}
}
