using PArticle.Application.Models.Auth;

namespace PArticle.Application.Features.Auth.Commands.Login
{
	public class LoginCommandResponse(string accessToken, string refreshToken) : LoginResponseModel(accessToken,refreshToken)
	{
	}
}
