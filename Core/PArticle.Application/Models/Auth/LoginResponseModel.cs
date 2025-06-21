namespace PArticle.Application.Models.Auth
{
	public class LoginResponseModel(string accessToken, string refreshToken)
	{
		public string AccessToken { get; set; } = accessToken;
		public string RefreshToken { get; set; } = refreshToken;
	}
}
