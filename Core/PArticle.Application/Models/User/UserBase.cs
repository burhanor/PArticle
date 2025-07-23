using Domain.Contracts.Enums;
using PArticle.Application.Models.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Models.User
{
	public class UserBase
	{
		public string EmailAddress { get; set; }
		public string Nickname { get; set; }
		public string Password { get; set; }
		public ImageModel? Image { get; set; }
		public bool IsActive { get; set; }
		public UserType UserType { get; set; }
	}
}
