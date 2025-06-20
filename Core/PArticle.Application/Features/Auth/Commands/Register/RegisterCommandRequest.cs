using MediatR;
using PArticle.Application.Models;
using PArticle.Application.Models.Auth;

namespace PArticle.Application.Features.Auth.Commands.Register
{
	public class RegisterCommandRequest:RegisterModel,IRequest<ResponseContainer<RegisterCommandResponse>>
	{
	}
}
