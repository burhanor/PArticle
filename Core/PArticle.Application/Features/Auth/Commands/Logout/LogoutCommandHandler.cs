using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Enums;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Auth.Commands.Logout
{


	public class LogoutCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.UserLogin>(uow, httpContextAccessor, mapper,rabbitMqService), IRequestHandler<LogoutCommandRequest, ResponseContainer<Unit>>
	{
		public async Task<ResponseContainer<Unit>> Handle(LogoutCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<Unit> response = new();

			string refreshToken = httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
			if (string.IsNullOrEmpty(refreshToken))
			{
				response.Status = ResponseStatus.Success;
				response.Message = Messages.Auth.LOGOUT_SUCCESS;
				return response;
			}
			writeRepository.Delete(m => m.RefreshToken == refreshToken);
			await uow.SaveChangesAsync(cancellationToken);

			httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.None, Expires = DateTime.UtcNow.AddDays(-1) });
			httpContextAccessor.HttpContext.Response.Cookies.Append("accessToken", "", new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.None, Expires = DateTime.UtcNow.AddDays(-1) });

			response.Status = ResponseStatus.Success;
			response.Message = Messages.Auth.LOGOUT_SUCCESS;

			return response;
		}
	}
}
