using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PArticle.Application.Extentions
{
	public static class StringExtension
	{
		public static string ToSlug(this string str)
		{
			if (string.IsNullOrWhiteSpace(str))
				return string.Empty;
			str = str.ToLowerInvariant().Trim();
			str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

			str = Regex.Replace(str, @"\s+", " ");

			if (str.Length > 50)
				str = str.Substring(0, 50).Trim();

			return Regex.Replace(str, @"\s", "-");
		}
	}
}
