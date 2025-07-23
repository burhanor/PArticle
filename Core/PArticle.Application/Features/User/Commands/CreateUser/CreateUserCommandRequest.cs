using MediatR;
using PArticle.Application.Models;
using PArticle.Application.Models.User;

namespace PArticle.Application.Features.User.Commands.CreateUser
{
	public class CreateUserCommandRequest:UserRequestModel,IRequest<ResponseContainer<CreateUserCommandResponse>>
	{
	}
}
