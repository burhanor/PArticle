﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PArticle.Application.Abstractions.Interfaces.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PArticle.Infrastructure.Token
{
	public class TokenService(IOptions<TokenModel> options) : ITokenService
	{
		private readonly TokenModel options = options.Value;
		public string GenerateAccessToken(UserDto user, IList<string>? roles)
		{
			List<Claim> claims = [
				new (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
				new ("id",user.Id.ToString()),
				new (JwtRegisteredClaimNames.Name,user.Nickname ?? string.Empty),
				new (JwtRegisteredClaimNames.Email,user.EmailAddress ?? string.Empty),
				new ("image",user.AvatarPath??string.Empty),
				new("userType",user.UserType),
				];
			if (roles?.Count > 0)
				claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
			SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(options.SecretKey));
			JwtSecurityToken token = new(
				issuer: options.Issuer,
				audience: options.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(options.TokenValidtyInMinutes),
				signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
				);

			return new JwtSecurityTokenHandler().WriteToken(token);

		}

		public string GenerateRefreshToken()
		{

			return Guid.NewGuid().ToString();
		}

		public ClaimsPrincipal? GetClaimsPrincipalFromToken(string token)
		{
			TokenValidationParameters tokenValidationParameters = new()
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
				ValidateIssuer = false,
				ValidIssuer = options.Issuer,
				ValidateAudience = false,
				ValidAudience = options.Audience,
				ValidateLifetime = false
			};
			JwtSecurityTokenHandler tokenHandler = new();
			ClaimsPrincipal? principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
			if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException("INVALID_TOKEN");
			return principal;
		}
	}
}
