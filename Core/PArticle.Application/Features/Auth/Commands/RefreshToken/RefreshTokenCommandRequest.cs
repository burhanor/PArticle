using MediatR;
using PArticle.Application.Models;
using PArticle.Application.Models.Auth;

namespace PArticle.Application.Features.Auth.Commands.RefreshToken
{
	public class RefreshTokenCommandRequest(string refreshToken,string cookieDomain) : IRequest<ResponseContainer<LoginResponseModel>>
	{
		public string RefreshToken { get; set; } = refreshToken;
		public string CookieDomain { get; set; } = cookieDomain;
	}
}
