using Domain.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Models.User
{
	public class UserResponseModel
	{
		public int Id { get; set; }
		public string Nickname { get; set; } = default!;
		public string EmailAddress { get; set; } = default!;
		public string? AvatarPath { get; set; }
		public bool IsActive { get; set; }
		public UserType UserType { get; set; }
	}
}
