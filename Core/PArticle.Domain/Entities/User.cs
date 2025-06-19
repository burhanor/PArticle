using PArticle.Domain.Concretes;
using PArticle.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Domain.Entities
{
	public class User:EntityBase
	{
		public string EmailAddress { get; set; }
		public string Nickname { get; set; }
		public string Password { get; set; }
		public string AvatarPath { get; set; }
		public bool IsActive { get; set; }
		public UserType UserType { get; set; }

		public virtual ICollection<Article> Articles { get; set; }

		public virtual ICollection<UserLogin> UserLogins { get; set; }
	}
}
