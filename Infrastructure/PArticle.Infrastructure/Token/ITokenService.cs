using PArticle.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PArticle.Infrastructure.Token
{
	public interface ITokenService
	{
		JwtSecurityToken GenerateAccessToken(User user, IList<string>? roles);
		string GenerateRefreshToken();
		ClaimsPrincipal? GetClaimsPrincipalFromToken(string token);
	}
}
