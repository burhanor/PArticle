using Domain.Contracts.Enums;
using PArticle.Application.Models.Image;
using PArticle.Application.Models.User;

namespace Particle.API.Models
{
	public  class AppUserModel
	{
		public string EmailAddress { get; set; }
		public string Nickname { get; set; }
		public string? Password { get; set; }
		public IFormFile? File { get; set; }
		public bool IsActive { get; set; }
		public UserType UserType { get; set; }

		public  UserRequestModel ToUserRequestModel( )
		{
			UserRequestModel request= new UserRequestModel
			{
				EmailAddress = EmailAddress,
				Nickname = Nickname,
				Password =Password,
				
				IsActive = IsActive,
				UserType = UserType
			};
			if (File != null)
			{
				request.Image = new ImageModel
				{
					FileName = File.FileName,
					ContentType =File.ContentType,
					Stream =File.OpenReadStream()
				};
			}
			else
			{
				request.Image = null;
			}

			return request;
		}
	}
}
