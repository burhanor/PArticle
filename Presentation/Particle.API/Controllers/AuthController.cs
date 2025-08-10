using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Particle.API.Extensions;
using PArticle.Application.Features.Auth.Commands.Login;
using PArticle.Application.Features.Auth.Commands.Logout;
using PArticle.Application.Features.Auth.Commands.RefreshToken;
using PArticle.Application.Features.Auth.Commands.Register;
using PArticle.Application.Models.Auth;

namespace Particle.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController(IMediator mediator, IMapper mapper,IConfiguration configuration) : ControllerBase
	{

		string cookieDomain = configuration["Cookie_Domain"]!;

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			RegisterCommandRequest request = mapper.Map<RegisterCommandRequest>(model);
			request.CookieDomain = cookieDomain;
			return await this.CreateAsync(mediator, request);
		}


		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login(LoginModel model)
		{

			LoginCommandRequest request = new(model.Nickname, model.Password,cookieDomain);
			return await this.OkAsync(mediator, request);
		}


		[HttpPost]
		[Route("refresh-token")]
		public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest request)
		{
			request.CookieDomain = cookieDomain;
			return await this.OkAsync(mediator, request);
		}


		[HttpPost]
		[Route("logout")]
		public async Task<IActionResult> Logout()
		{
			LogoutCommandRequest request = new();
			request.CookieDomain = cookieDomain;
			return await this.OkAsync(mediator, request);
		}
	}
}
