using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Models.Auth
{
	public class RegisterModel
	{
		public string EmailAddress { get; set; } = default!;
		public string Password { get; set; } = default!;
		public string Nickname { get; set; } = default!;
	}
}
