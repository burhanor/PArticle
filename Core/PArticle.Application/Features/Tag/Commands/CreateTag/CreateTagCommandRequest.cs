using MediatR;
using PArticle.Application.Models;
using PArticle.Application.Models.Tag;

namespace PArticle.Application.Features.Tag.Commands.CreateTag
{
	public class CreateTagCommandRequest: TagDto, IRequest<ResponseContainer<CreateTagCommandResponse>>
	{
	}
}
