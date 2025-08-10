using MediatR;
using PArticle.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Auth.Commands.Login
{
	public class LoginCommandRequest : IRequest<ResponseContainer<LoginCommandResponse>>
	{
		public LoginCommandRequest()
		{

		}
		public LoginCommandRequest(string emailOrNickname, string password,string cookieDomain) 
		{
			EmailOrNickname = emailOrNickname;
			Password = password;
			CookieDomain = cookieDomain;
		}

		public string EmailOrNickname { get; set; }
		public string Password { get; set; }
		public string CookieDomain { get; set; }
	}
}
