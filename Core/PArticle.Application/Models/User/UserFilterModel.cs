using Domain.Contracts.Enums;

namespace PArticle.Application.Models.User
{
	public class UserFilterModel:FilterModel
	{
		public string? Nickname { get; set; }
		public string? EmailAddress { get; set; }
		public UserType? UserType { get; set; }
		public bool? IsActive { get; set; }
	}
}
