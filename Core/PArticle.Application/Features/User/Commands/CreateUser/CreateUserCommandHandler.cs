using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.FileStorage;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Enums;
using PArticle.Application.Helpers;
using PArticle.Application.Models;

namespace PArticle.Application.Features.User.Commands.CreateUser
{
	public class CreateUserCommandHandler(IUow uow, IMapper mapper, IHttpContextAccessor httpContextAccessor,IFileStorageService fileStorageService) : BaseHandler<Domain.Entities.User>(uow, httpContextAccessor, mapper), IRequestHandler<CreateUserCommandRequest, ResponseContainer<CreateUserCommandResponse>>
	{
		public async Task<ResponseContainer<CreateUserCommandResponse>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
		{

			ResponseContainer<CreateUserCommandResponse> response = await ValidationHelper.ValidateAsync<CreateUserCommandRequest, CreateUserCommandResponse, CreateUserCommandValidator>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool emailIsUnique = await readRepository.UniqueAsync(m => m.EmailAddress == request.EmailAddress, cancellationToken);
			if (!emailIsUnique)
			{
				response.AddValidationError(nameof(request.EmailAddress), Messages.User.EMAIL_ALREAY_EXIST);
				return response;
			}
			bool nicknameUnique = await readRepository.UniqueAsync(m => m.Nickname == request.Nickname, cancellationToken);
			if (!nicknameUnique)
			{
				response.AddValidationError(nameof(request.Nickname), Messages.User.NICKNAME_ALREADY_EXIST);
				return response;
			}

			


			Domain.Entities.User user = mapper.Map<Domain.Entities.User>(request);

			if (request.Image != null)
			{
				user.AvatarPath = await fileStorageService.SaveFileAsync(request.Image.Stream,request.Image.FileName);
			}
			else
			{
				user.AvatarPath = string.Empty;
			}

				user.Password = HashHelper.HashPassword(request.Password);
			await writeRepository.AddAsync(user, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (user.Id > 0)
			{
				response.Data = mapper.Map<CreateUserCommandResponse>(user);
				response.Message = Messages.User.CREATE_SUCCESS;
				response.Status = ResponseStatus.Success;
			}
			else
			{
				response.Message = Messages.User.CREATE_FAILED;
				response.Status = ResponseStatus.Failed;
			}

			return response;


		}
	}
}
