using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Enums;
using PArticle.Application.Helpers;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Tag.Commands.CreateTag
{


	public class CreateTagCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.Tag>(uow, httpContextAccessor, mapper), IRequestHandler<CreateTagCommandRequest, ResponseContainer<CreateTagCommandResponse>>
	{
		public async Task<ResponseContainer<CreateTagCommandResponse>> Handle(CreateTagCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<CreateTagCommandResponse> response = await ValidationHelper.ValidateAsync<CreateTagCommandRequest, CreateTagCommandResponse, CreateTagCommandValidator>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool nameUnique = await readRepository.UniqueAsync(m => m.Name == request.Name, cancellationToken);
			if (!nameUnique)
			{
				response.AddValidationError(nameof(request.Name), Messages.Tag.TAG_NAME_ALREADY_EXIST);
				return response;
			}

			bool slugUnique = await readRepository.UniqueAsync(m => m.Slug == request.Slug, cancellationToken);
			if (!slugUnique)
			{
				response.AddValidationError(nameof(request.Slug), Messages.Tag.TAG_SLUG_ALREADY_EXIST);
				return response;
			}

			Domain.Entities.Tag tag = mapper.Map<Domain.Entities.Tag>(request);
			await writeRepository.AddAsync(tag, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (tag.Id > 0)
			{
				response.Data = mapper.Map<CreateTagCommandResponse>(tag);
				response.Message = Messages.Tag.TAG_CREATE_SUCCESS;
				response.Status = ResponseStatus.Success;
			}
			else
			{
				response.Message = Messages.Tag.TAG_CREATE_FAILED;
				response.Status = ResponseStatus.Failed;
			}
			return response;
		}
	}
}
