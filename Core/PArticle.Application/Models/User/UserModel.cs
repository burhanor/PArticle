using Domain.Contracts.Enums;
using Domain.Contracts.Interfaces;

namespace PArticle.Application.Models.User
{
	public class UserModel: IEntityBase
	{
		public int Id { get; set; }
		public string Nickname { get; set; } = default!;
		public string EmailAddress { get; set; } = default!;
		public string Password { get; set; } = default!;
		public string? Info { get; set; }
		public string? Avatar { get; set; }
		public UserType UserType { get; set; }
	}
}
