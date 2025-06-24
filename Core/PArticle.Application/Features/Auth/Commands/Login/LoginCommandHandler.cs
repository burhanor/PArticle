using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Repositories;
using PArticle.Application.Abstractions.Interfaces.Token;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Enums;
using PArticle.Application.Extentions;
using PArticle.Application.Helpers;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Auth.Commands.Login
{
	public class LoginCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITokenService tokenService,IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.User>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<LoginCommandRequest, ResponseContainer<LoginCommandResponse>>
	{
		private readonly ITokenService tokenService = tokenService;

		public async Task<ResponseContainer<LoginCommandResponse>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
		{
			LoginCommandValidator validationRules = new();
			ValidationResult validationResult = await validationRules.ValidateAsync(request, cancellationToken);
			ResponseContainer<LoginCommandResponse> response = validationResult.ToResponse<LoginCommandResponse>();
			if (response.Status == ResponseStatus.ValidationError)
				return response;
			Domain.Entities.User? user = await readRepository.GetAsync(m => (m.EmailAddress == request.EmailOrNickname || m.Nickname == request.EmailOrNickname) && m.IsActive  && m.Password == HashHelper.HashPassword(request.Password), cancellationToken: cancellationToken);
			if (user == null)
			{
				response.Message = Messages.User.USER_NOT_FOUND;
				return response;
			}
			response.Message = Messages.Auth.LOGIN_SUCCESS;
			response.Status = ResponseStatus.Success;

			UserDto userDto = mapper.Map<UserDto>(user);
			string accessToken = tokenService.GenerateAccessToken(userDto, [user.UserType.ToString()]);
			string refreshToken = tokenService.GenerateRefreshToken();
			response.Data = new LoginCommandResponse(accessToken, refreshToken);

			IWriteRepository<Domain.Entities.UserLogin> tokenWriteRepository = uow.GetWriteRepository<Domain.Entities.UserLogin>();
			await tokenWriteRepository.AddAsync(new Domain.Entities.UserLogin
			{
				UserId = user.Id,
				RefreshToken = refreshToken,
				AccessToken = accessToken,
				LoginDate = DateTime.UtcNow,
				IpAddress=ipAddress,
				
			}, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);

			if (httpContextAccessor.HttpContext != null)
			{
				httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.None, Expires = DateTime.UtcNow.AddMonths(3) });
				httpContextAccessor.HttpContext.Response.Cookies.Append("accessToken", accessToken, new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.None, Expires = DateTime.UtcNow.AddDays(3) });
			}

			return response;
		}
	}
}
