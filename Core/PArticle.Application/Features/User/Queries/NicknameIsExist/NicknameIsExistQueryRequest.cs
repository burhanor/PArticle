using MediatR;

namespace PArticle.Application.Features.User.Queries.NicknameIsExist
{
	

	public class NicknameIsExistQueryRequest(string nickname) : IRequest<bool>
	{
		public string Nickname { get; set; } = nickname;
	}
}
