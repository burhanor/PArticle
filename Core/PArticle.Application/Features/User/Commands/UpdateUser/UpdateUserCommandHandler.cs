using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Enums;
using PArticle.Application.Abstractions.Interfaces.FileStorage;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Enums;
using PArticle.Application.Helpers;
using PArticle.Application.Models;

namespace PArticle.Application.Features.User.Commands.UpdateUser
{
	public class UpdateUserCommandHandler(IUow uow, IMapper mapper, IHttpContextAccessor httpContextAccessor,IFileStorageService fileStorageService) : BaseHandler<Domain.Entities.User>(uow, httpContextAccessor, mapper), IRequestHandler<UpdateUserCommandRequest, ResponseContainer<UpdateUserCommandResponse>>
	{
		public async Task<ResponseContainer<UpdateUserCommandResponse>> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<UpdateUserCommandResponse> response = await ValidationHelper.ValidateAsync<UpdateUserCommandRequest, UpdateUserCommandResponse, UpdateUserCommandValidator>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;
			Domain.Entities.User? oldUser = await readRepository.FindAsync(request.Id, cancellationToken: cancellationToken);
			if (oldUser == null)
			{
				response.Message = Messages.User.USER_NOT_FOUND;
				return response;
			}
			bool nicknameUnique = await readRepository.UniqueAsync(m => m.Nickname == request.Nickname && m.Id != request.Id, cancellationToken);
			if (!nicknameUnique)
			{
				response.AddValidationError(nameof(request.Nickname), Messages.User.NICKNAME_ALREADY_EXIST);
				return response;
			}
			bool emailUnique = await readRepository.UniqueAsync(m => m.EmailAddress == request.EmailAddress && m.Id != request.Id, cancellationToken);
			if (!emailUnique)
			{
				response.AddValidationError(nameof(request.EmailAddress), Messages.User.EMAIL_ALREAY_EXIST);
				return response;
			}
			
			Domain.Entities.User user = mapper.Map<Domain.Entities.User>(request);

			if (request.Image != null)
			{
				user.AvatarPath = await fileStorageService.SaveFileAsync(request.Image.Stream, request.Image.FileName);
			}
			else
			{
				user.AvatarPath = oldUser.AvatarPath;
			}
			user.Password = string.IsNullOrEmpty(request.Password) ? oldUser.Password : HashHelper.HashPassword(request.Password);

			if (!writeRepository.Update(user))
			{
				response.Message = Messages.User.USER_NOT_FOUND;
				return response;
			}
			await uow.SaveChangesAsync(cancellationToken);

			response.Data = mapper.Map<UpdateUserCommandResponse>(user);
			response.Message = Messages.User.UPDATE_SUCCESS;
			response.Status = ResponseStatus.Success;
			return response;
		}



	}
}
