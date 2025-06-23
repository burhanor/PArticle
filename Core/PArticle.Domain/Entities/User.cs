using Domain.Contracts.Enums;
using PArticle.Domain.Concretes;

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
