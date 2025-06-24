using AutoMapper;
using Domain.Contracts.Enums;
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

namespace PArticle.Application.Features.Auth.Commands.Register
{
	public class RegisterCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITokenService tokenService,IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.User>(uow, httpContextAccessor, mapper,rabbitMqService), IRequestHandler<RegisterCommandRequest, ResponseContainer<RegisterCommandResponse>>
	{
		public async Task<ResponseContainer<RegisterCommandResponse>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
		{
			RegisterCommandValidator validationRules = new();
			ValidationResult validationResult = await validationRules.ValidateAsync(request, cancellationToken);
			ResponseContainer<RegisterCommandResponse> response = validationResult.ToResponse<RegisterCommandResponse>();
			if (response.Status == ResponseStatus.ValidationError)
				return response;
			
			bool emailUnique = await readRepository.UniqueAsync(m => m.EmailAddress == request.EmailAddress, cancellationToken);
			if (!emailUnique)
			{
				response.AddValidationError(nameof(request.EmailAddress), Messages.Auth.EMAIL_ALREAY_EXIST);
				return response;
			}
			bool nicknameUnique = await readRepository.UniqueAsync(m => m.Nickname == request.Nickname, cancellationToken);
			if (!nicknameUnique)
			{
				response.AddValidationError(nameof(request.Nickname), Messages.Auth.NICKNAME_ALREADY_EXIST);
				return response;
			}
			Domain.Entities.User user = mapper.Map<Domain.Entities.User>(request);
			user.AvatarPath ??= string.Empty;
			user.IsActive = true;
			user.UserType = UserType.Author;
			user.Password = HashHelper.HashPassword(request.Password);
			await writeRepository.AddAsync(user, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (user.Id > 0)
			{
				response.Data = mapper.Map<RegisterCommandResponse>(user);
				response.Message = Messages.Auth.REGISTER_SUCCESS;
				response.Status = ResponseStatus.Success;
				UserDto userDto = mapper.Map<UserDto>(user);
				string accessToken = tokenService.GenerateAccessToken(userDto, [user.UserType.ToString()]);
				string refreshToken = tokenService.GenerateRefreshToken();

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
			}
			else
			{
				response.Message = Messages.Auth.REGISTER_FAILED;
				response.Status = ResponseStatus.Failed;
			}
			return response;
		}
	}
}
