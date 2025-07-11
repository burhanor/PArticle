using MediatR;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Auth.Commands.RefreshToken
{
	public class RefreshTokenCommandRequest(string refreshToken) : IRequest<ResponseContainer<Unit>>
	{
		public string RefreshToken { get; set; } = refreshToken;
	}
}
