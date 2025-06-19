using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Infrastructure.Token
{
	public class TokenModel
	{
		public string Audience { get; set; } = string.Empty;
		public string Issuer { get; set; } = string.Empty;
		public string SecretKey { get; set; } = string.Empty;
		public int TokenValidtyInMinutes { get; set; }
		public int RefreshTokenValidtyInDays { get; set; }
	}
}
