using System.Security.Claims;

namespace PArticle.Application.Abstractions.Interfaces.Token
{
	public interface ITokenService
	{
		string GenerateAccessToken(UserDto user, IList<string>? roles);
		string GenerateRefreshToken();
		ClaimsPrincipal? GetClaimsPrincipalFromToken(string token);
	}
}
