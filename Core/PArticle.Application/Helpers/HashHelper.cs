using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Helpers
{
	public static class HashHelper
	{
		public static string HashPassword(string password)
		{
			var bytes = Encoding.UTF8.GetBytes(password);
			var hash = SHA512.HashData(bytes);
			return Convert.ToHexStringLower(hash);
		}

		public static bool VerifyPassword(string password, string hashedPassword) => HashPassword(password) == hashedPassword;
	}
}
