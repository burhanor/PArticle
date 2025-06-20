using MediatR;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Auth.Commands.Logout
{
	public class LogoutCommandRequest : IRequest<ResponseContainer<Unit>>
	{
	}
}
