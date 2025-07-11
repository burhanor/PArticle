using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Repositories;
using PArticle.Application.Abstractions.Interfaces.Token;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Enums;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Auth.Commands.RefreshToken
{
	public class RefreshTokenCommandHandler(IUow uow,IHttpContextAccessor httpContextAccessor,IMapper mapper, ITokenService tokenService) :BaseHandler<Domain.Entities.UserLogin>(uow,httpContextAccessor,mapper),IRequestHandler<RefreshTokenCommandRequest, ResponseContainer<Unit>>
	{
		public async Task<ResponseContainer<Unit>> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<Unit> response = new ResponseContainer<Unit>
			{
				Message = Messages.Auth.LOGIN_FAILED,
				Status = ResponseStatus.Failed
			};
			Domain.Entities.UserLogin? userLogin= await readRepository.GetAsync(m => m.RefreshToken == request.RefreshToken ,enableTracking:true, cancellationToken: cancellationToken);
			if (userLogin == null)
				return response;
			Domain.Entities.User? user = await uow.GetReadRepository<Domain.Entities.User>().GetAsync(m =>  m.IsActive && m.Id==userLogin.UserId, cancellationToken: cancellationToken);
			if (user== null)
				return response;

			UserDto userDto = mapper.Map<UserDto>(user);
			string accessToken = tokenService.GenerateAccessToken(userDto, [user.UserType.ToString()]);
			string refreshToken = tokenService.GenerateRefreshToken();
			userLogin.AccessToken = accessToken;
			userLogin.RefreshToken = refreshToken;
			await uow.SaveChangesAsync(cancellationToken);
			response.Status = ResponseStatus.Success;
			response.Message = Messages.Auth.LOGIN_SUCCESS;
			if (httpContextAccessor.HttpContext != null)
			{
				httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.None, Expires = DateTime.UtcNow.AddMonths(3) });
				httpContextAccessor.HttpContext.Response.Cookies.Append("accessToken", accessToken, new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.None, Expires = DateTime.UtcNow.AddDays(3) });
			}

			return response;
		}
	}	
}
