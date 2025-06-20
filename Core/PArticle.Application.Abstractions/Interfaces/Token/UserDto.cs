using Domain.Contracts.Interfaces;

namespace PArticle.Application.Abstractions.Interfaces.Token
{
	public class UserDto: IEntityBase
	{
		public int Id { get; set; }
		public string EmailAddress { get; set; }
		public string Nickname { get; set; }
		public string Password { get; set; }
		public string AvatarPath { get; set; }
		public bool IsActive { get; set; }
		public string UserType { get; set; }
	}
}
